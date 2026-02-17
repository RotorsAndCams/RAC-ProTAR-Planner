using GMap.NET;
using GMap.NET.Internals;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner;
using MissionPlanner.ArduPilot;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using MissionPlanner.Utilities.HW;
using NLog;
using ptPlugin1.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MissionPlanner.Utilities.adsb;
using static MissionPlanner.Utilities.Device;
using static MissionPlanner.Utilities.LTM;

namespace ptPlugin1
{
    #region UI

    public partial class AutoLandTester : UserControl
    {
        DateTime t0 = DateTime.MinValue;
        TimeSpan deltaT = TimeSpan.FromSeconds(60);
        int displayedPlane = -1; 

        class ComboItemColor
        {
            public Color color { get; set; }
            public string text { get; set; }
        }

        class ComboItemPoint
        {
            public PointLatLngAlt point { get; set; }
            public string text { get; set; }
        }

        public AutoLandTester()
        {
            InitializeComponent();

            CB_LandingZones.DisplayMember = "text";
            CB_LandingZones.ValueMember = "point";
            CB_LandingZones.DataSource = new ComboItemPoint[]
            {
                new ComboItemPoint() { point = new PointLatLngAlt(), text = "Primer (empty)" },
                new ComboItemPoint() { point = new PointLatLngAlt(), text = "Alternate (empty)" },
                new ComboItemPoint() { point = new PointLatLngAlt(), text = "Contingency (empty)" },
                new ComboItemPoint() { point = new PointLatLngAlt(), text = "Dynamic (empty)" },
            };
            //CB_LandingZones.SelectedIndex = 0;

            CB_colors.DisplayMember = "text";
            CB_colors.ValueMember = "color";
            CB_colors.DataSource = new ComboItemColor[]
            {
                new ComboItemColor() { color = Color.White, text = "White" },
                new ComboItemColor() { color = Color.Orange, text = "Orange" },
                new ComboItemColor() { color = Color.Blue, text = "Blue" },
            };
        }

        private void bStartLanding_Click(object sender, EventArgs e)
        {
            if (!LandingHandler.Instance.planes.ContainsKey(MainV2.comPort.sysidcurrent))
                return;
            LandingHandler.Instance.planes[MainV2.comPort.sysidcurrent].startLandingProcess();
            LandingLogger.GetInstance().Info("Start landing button clicked", MainV2.comPort.sysidcurrent);
        }

        private void bAbortLanding_Click(object sender, EventArgs e)
        {
            if (!LandingHandler.Instance.planes.ContainsKey(MainV2.comPort.sysidcurrent))
                return;
            LandingHandler.Instance.planes[MainV2.comPort.sysidcurrent].abortLandingProcess();
            LandingLogger.GetInstance().Info("Abort landing button clicked", MainV2.comPort.sysidcurrent);
        }

        // Called from ptPlugin1 from loop()
        public void updateLabels()
        {            
            deltaT = DateTime.Now - t0;
            lLastUpdate.Text = $"Last update: {deltaT} sec";
            t0 = DateTime.Now;

            if (LandingHandler.Instance.planes.ContainsKey(MainV2.comPort.sysidcurrent)) 
            {
                int plane = MainV2.comPort.sysidcurrent;
                
                // Current landing state
                lState.Text = $"Landing state: {LandingHandler.Instance.planes[plane].state}";

                // Distance to target
                lDistanceToTarget.Text = $"Distance to target: {LandingHandler.Instance.planes[plane].testDist:0.0} m";

                // Disable turn direction change during landing process
                CHK_ClockwiseTurn.Enabled = (LandingHandler.Instance.planes[plane].state == LandingState.None);
            }
            else
            {
                // Distance to target
                lDistanceToTarget.Text = "Distance to target: N/A";
            }
        }

        // If the active plane changes update menu called from loop() TODO
        public void updatePanelToActivePlane()
        {
            int plane = MainV2.comPort.sysidcurrent;

            // If active plane is displayed skip update
            if (plane == displayedPlane)
            {
                return;
            }

            if (LandingHandler.Instance.planes.ContainsKey(plane))
            {
                // Update only if the plane is in the landing handler
                displayedPlane = plane;

                // Active plane ID
                lActivePlaneID.Text = $"Active plane: {plane}";

                // Turn direction
                if (LandingHandler.Instance.planes[plane].turnDirection == TurnDirection.clockwise)
                {
                    CHK_ClockwiseTurn.Checked = true;
                }
                else
                {
                    CHK_ClockwiseTurn.Checked = false;
                }

                // Approach overrides
                CHK_ApproachOverride.Checked = LandingHandler.Instance.planes[plane].overrideApprroach;
                UpDwn_Distance.Value = LandingHandler.Instance.planes[plane].overrideApproachDistance;
                UpDwn_Direction.Value = LandingHandler.Instance.planes[plane].overrideApproachDir;

                // Wind measurement time
                numericUpDown1.Value = Convert.ToInt32(LandingHandler.Instance.planes[plane].maxMeasurementTime.TotalSeconds);

                // Airspeed params
                UpDwn_CruiseSpeed.Value = LandingHandler.Instance.planes[plane].cruiseAirSpeed;
                UpDwn_MeasSpeed.Value = LandingHandler.Instance.planes[plane].measurementAirSpeed;
                UpDwn_LineupSpeed.Value = LandingHandler.Instance.planes[plane].lineupAirSpeed;
                UpDwn_FinalSpeed.Value = LandingHandler.Instance.planes[plane].approachAirSpeed;

                // Color
                CB_colors.SelectedValue = LandingHandler.Instance.planes[plane].displayColor;

                // Lz combo box
                int selected_Lz_index = 0;
                bool found = false;
                foreach (ComboItemPoint item in CB_LandingZones.Items)
                {
                    if ((item.point == LandingHandler.Instance.planes[plane].LandingPoint) && 
                        (new PointLatLngAlt() != LandingHandler.Instance.planes[plane].LandingPoint))
                    {
                        found = true;
                        break;
                    }
                    selected_Lz_index ++;
                }
                if (!found)
                {
                    selected_Lz_index = 0; // Default to primer
                }

                CB_LandingZones.SelectedIndex = selected_Lz_index;

                // Set the primary Lz if the selected plane do not already has one
                if (!found)
                {
                    LandingHandler.Instance.planes[plane].LandingPoint = (PointLatLngAlt)CB_LandingZones.SelectedValue;
                }

                // Marker tooltip
                string selected_Lz_tag = $"Lz {CB_LandingZones.SelectedIndex + 1}";
                MainV2.instance.BeginInvoke((MethodInvoker)(() =>
                {
                    // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                    var map = MainV2.instance.FlightData.gMapControl1;
                    if (map == null) return;

                    foreach (int key in LandingHandler.Instance.landingPointOverlays.Keys)
                    {
                        GMapOverlay overlay = LandingHandler.Instance.landingPointOverlays[key];
                        foreach (var marker in overlay.Markers)
                        {
                            if (marker.Tag != null && marker.Tag.Equals(selected_Lz_tag) && plane == key)
                                marker.ToolTipMode = MarkerTooltipMode.Always;
                            else
                                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        }
                    }

                    map.Refresh();
                    // Updates the map and place the marker to the correct position (refresh does not work)
                    map.Position = map.Position;
                }));

            }
            else
            {
                // Active plane ID
                lActivePlaneID.Text = $"Active plane: N/A";

                // Current landing state
                lState.Text = "Landing state: N/A";

                // Airspeed params
                UpDwn_CruiseSpeed.Value = 0;  
                UpDwn_MeasSpeed.Value = 0;
                UpDwn_LineupSpeed.Value = 0;
                UpDwn_FinalSpeed.Value = 0;
            }
        }

        //--------------------------------
        // Landing zones
        //--------------------------------
        // Add / Update points in the landing zone combo box
        public void addLzToCB_LandingZones(PointLatLngAlt point)
        {
            int planeID = MainV2.comPort.sysidcurrent;
            int i = CB_LandingZones.SelectedIndex;

            // Get the current array
            var data = (ComboItemPoint[])CB_LandingZones.DataSource;

            // Update text to remove (empty) tag
            string text_ = " ";
            switch (i)
            {
                case 0: text_ = $"Primer"; break;
                case 1: text_ = $"Alternate"; break;
                case 2: text_ = $"Contingency"; break;
                case 3: text_ = $"Dynamic"; break;
            }

            data[i] = new ComboItemPoint
            {
                point = point,
                text = text_
            };

            // Refresh ComboBox binding to show update
            CB_LandingZones.DataSource = null;
            CB_LandingZones.DataSource = data;
            CB_LandingZones.DisplayMember = "Text";
            CB_LandingZones.ValueMember = "Point";

            LandingLogger.GetInstance().Info($"{text_} landing zone added to coords: {point}", planeID);
        }

        public void addLzMarkerToMap()
        {
            int planeID = MainV2.comPort.sysidcurrent;
            int i = CB_LandingZones.SelectedIndex +1;

            GMarkerGoogleType markerType = LandingHandler.Instance.planes[planeID].getMarkerType();

            GMarkerGoogle markerLanding = new GMarkerGoogle(LandingHandler.Instance.planes[planeID].LandingPoint, markerType);

            // Display tooltip text on map next to marker
            markerLanding.ToolTipText = $"Lz {i}";
            markerLanding.ToolTipMode = MarkerTooltipMode.Always;
            markerLanding.Tag = $"Lz {i}";

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(planeID, out var overlay))
                    return;

                // Marker placement
                var marker_old = overlay.Markers
        .                       FirstOrDefault(m => m.Tag != null && m.Tag.Equals($"Lz {i}"));

                if (marker_old != null)
                {
                    overlay.Markers.Remove(marker_old);
                }

                LandingHandler.Instance.landingPointOverlays[planeID].Routes.Clear();
                LandingHandler.Instance.landingPointOverlays[planeID].Polygons.Clear();

                overlay.Markers.Add(markerLanding);
                map.Overlays.Add(LandingHandler.Instance.landingPointOverlays[planeID]);

                map.Refresh();
                // Updates the map and place the marker to the correct position (refresh does not work)
                map.Position = map.Position;
            }));
        }

        #region Overrides

        //--------------------------------
        // Approach point
        //--------------------------------
        private void CHK_ApproachOverride_CheckedChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override flag in the landing handler
            LandingHandler.Instance.planes[key].overrideApprroach = CHK_ApproachOverride.Checked;

            if (CHK_ApproachOverride.Checked)
            {
                LandingLogger.GetInstance().Info($"Approach override enabled", key);
            }
            else
            {
                LandingLogger.GetInstance().Info($"Approach override disabled", key);
            }
        }

        private void UpDwn_Distance_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override distance in the landing handler
            LandingHandler.Instance.planes[key].overrideApproachDistance = Convert.ToInt32(UpDwn_Distance.Value);

        }

        private void UpDwn_Direction_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override distance in the landing handler
            LandingHandler.Instance.planes[key].overrideApproachDir = Convert.ToInt32(UpDwn_Direction.Value);
        }
        
        //--------------------------------
        // Loitering direction
        //--------------------------------
        private void CHK_ClockwiseTurn_CheckedChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override flag in the landing handler
            if (CHK_ClockwiseTurn.Checked)
            {
                LandingHandler.Instance.planes[key].turnDirection = TurnDirection.clockwise;
                LandingLogger.GetInstance().Info($"Clockwise turn set", key);
            }
            else
            {
                LandingHandler.Instance.planes[key].turnDirection = TurnDirection.counterClockwise;
                LandingLogger.GetInstance().Info($"Counter-clockwise turn set", key);
            }
        }

        //--------------------------------
        // Wind measuremnent time
        //--------------------------------
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Override measurement time of the current plane
            LandingHandler.Instance.planes[key].maxMeasurementTime = TimeSpan.FromSeconds(Convert.ToInt32(numericUpDown1.Value));
        }

        //--------------------------------
        // Airspeeds
        //--------------------------------
        private void UpDwn_CruiseSpeed_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override
            LandingHandler.Instance.planes[key].cruiseAirSpeed = Convert.ToInt32(UpDwn_CruiseSpeed.Value);
            Settings.Instance["Protar_cruiseAirSpeed"] = UpDwn_CruiseSpeed.Value.ToString();
        }

        private void UpDwn_MeasSpeed_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override
            LandingHandler.Instance.planes[key].measurementAirSpeed = Convert.ToInt32(UpDwn_MeasSpeed.Value);
            Settings.Instance["Protar_measurementAirSpeed"] = UpDwn_MeasSpeed.Value.ToString();
        }

        private void UpDwn_LineupSpeed_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override
            LandingHandler.Instance.planes[key].lineupAirSpeed = Convert.ToInt32(UpDwn_LineupSpeed.Value);
            Settings.Instance["Protar_lineupAirSpeed"] = UpDwn_LineupSpeed.Value.ToString();
        }

        private void UpDwn_FinalSpeed_ValueChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override
            LandingHandler.Instance.planes[key].approachAirSpeed = Convert.ToInt32(UpDwn_FinalSpeed.Value);
            Settings.Instance["Protar_approachAirSpeed"] = UpDwn_FinalSpeed.Value.ToString();
        }

        private void bForceNextStage_Click(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int planeID = MainV2.comPort.sysidcurrent;
            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(planeID)) return;

            // Check window to avoid accidental clicks
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to force the next stage? This may cause unexpected behavior if the plane is not in the expected state.", "Force next stage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            
            LandingHandler.Instance.planes[planeID].forceNextStage = true;
            LandingLogger.GetInstance().Info($"Force next stage", planeID);
        }

        # endregion Overrides

        //--------------------------------
        // Color selection
        //--------------------------------
        private void CB_colors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int key = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(key))
            {
                return;
            }

            // Set override distance in the landing handler
            LandingHandler.Instance.planes[key].displayColor = (Color)CB_colors.SelectedValue;

            // Update map drawings
            LandingHandler.Instance.planes[key].updateDrawingColor();

        }

        private void CB_LandingZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the plane's AutoLanding instance
            int planeID = MainV2.comPort.sysidcurrent;

            // Check if the plane exists in the landing handler
            if (!LandingHandler.Instance.planes.ContainsKey(planeID)) return;

            // Remove tooltip from other markers and add to the selected one
            string selected_Lz_tag = $"Lz {CB_LandingZones.SelectedIndex + 1}";
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                foreach (int key in LandingHandler.Instance.landingPointOverlays.Keys)
                { 
                    GMapOverlay overlay = LandingHandler.Instance.landingPointOverlays[key];
                    foreach (var marker in overlay.Markers)
                    {
                        if (marker.Tag != null && marker.Tag.Equals(selected_Lz_tag) && planeID == key)
                            marker.ToolTipMode = MarkerTooltipMode.Always;
                        else
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    }
                }

                map.Refresh();
                // Updates the map and place the marker to the correct position (refresh does not work)
                map.Position = map.Position;
            }));

            // Check if a valid point is selected
            if ((PointLatLngAlt)CB_LandingZones.SelectedValue == new PointLatLngAlt()) return;

            // Set landing point from the selected combo box item
            LandingHandler.Instance.planes[planeID].LandingPoint = (PointLatLngAlt)CB_LandingZones.SelectedValue;
        }
    }
    #endregion

    #region AutoLanding Logic
    public enum LandingState
    {
        // Landing is not performed
        None,
        // Before start to fly to landing position fly up to a safe RTL altitude
        UpToRTLAltitude,
        // Go to the landing point
        GoToLandingPoint,
        // Prepare for measurement loitering
        PrepareForMeasurement,
        // Performe wind measurement
        MeasureWind,
        // Wait until reaching the transition point to start lineup maneuver
        WaitForTransitionPoint,
        // Go to the lineup bank point
        GoToLineupCircle,
        // Loiter around the turn point to lineup for landing
        LineupForLanding,
        // Aproaching the landing point
        GoToLand,
        
        Landed,
        Error,
    }

    // In Mission Planner, the turning direction when loitering is determined by the sign of the loiter radius.
    public enum TurnDirection
    {
        undefined = 0,
        clockwise = 1,
        counterClockwise = -1
    }

    // This handler simulates future plane class containing the landing logic
    public sealed class LandingHandler
    {
        // Setup Singleton
        public static readonly LandingHandler _instance = new LandingHandler();
        public static LandingHandler Instance => _instance;
        private LandingHandler() { }

        // Dictionary to track which AutoLanding instance belongs to which plane
        public Dictionary<int, AutoLanding> planes = new Dictionary<int, AutoLanding>();

        // Dictionary to track which landing point markers belongs to which plane
        public Dictionary<int, GMapOverlay> landingPointOverlays = new Dictionary<int, GMapOverlay>();
    }

    // Defines a restricted map area where landing points are not allowed to be placed and planes must avoid during final approach.
    public sealed class BaseZone
    {
        // Setup Singleton
        public static readonly BaseZone _instance = new BaseZone();
        public static BaseZone Instance => _instance;
        private BaseZone() { }

        // The position of the operators (shared among all planes)
        public List<PointLatLngAlt> pointList = new List<PointLatLngAlt>();
        public bool isSet = false;
        public DateTime timeOfLastBasePoint = DateTime.MinValue;
        private double fadeAwayTime = TimeSpan.FromSeconds(10).TotalMilliseconds;
        private TurnDirection placementDirection = TurnDirection.undefined;

        public bool covers(PointLatLngAlt Point)
        {
            int N = pointList.Count;

            if (N < 3) return false;

            TurnDirection dir1 = getPointDirection(pointList[N-1],
                              pointList[0],
                              Point);

            for (int i = 0; i <= N-2; i++)
            {
                TurnDirection dir2 = getPointDirection(pointList[i],
                              pointList[i+1],
                              Point);

                if (dir2 != dir1) 
                {
                    return false;
                }
                dir1 = dir2;
            }
            return true;
        }

        public bool isConcaveWith(PointLatLngAlt Point)
        {
            // Check if the new point creates a concave polygon

            int N = pointList.Count-1;
            double diff;

            // Skip if not enough base points
            if (N <= 1)
            {
                return false;
            }

            // Set the base point placement direction using the last two bearings
            // (handles the case where the first points are collinear).
            if (placementDirection == TurnDirection.undefined)
            {
                TurnDirection dir = getPointDirection(pointList[N - 2],
                                                      pointList[N - 1],
                                                      pointList[N]);

                if (dir == TurnDirection.undefined)
                {
                    return false;
                }
                placementDirection = dir;
            }

            TurnDirection dir1 = getPointDirection(pointList[N - 1],
                                      pointList[N],
                                      Point);

            TurnDirection dir2 = getPointDirection(pointList[N],
                          Point,
                          pointList[0]);

            TurnDirection dir3 = getPointDirection(Point,
              pointList[0],
              pointList[1]);

            // Return true if the new point creates a concave polygon
            return dir1 != placementDirection || dir2 != placementDirection || dir3 != placementDirection;
        }

        private TurnDirection getPointDirection(PointLatLngAlt basePoint, PointLatLngAlt Point1, PointLatLngAlt Point2)
        {
            double bearing1 = basePoint.GetBearing(Point1);
            double bearing2 = basePoint.GetBearing(Point2);
            double diff = angleDifference((float)bearing2, (float)bearing1);

            if (diff == 0) return TurnDirection.undefined;
            else if (diff > 0) return TurnDirection.clockwise;
            else return TurnDirection.counterClockwise;
        }

        private float angleDifference(float angle, float baseAngle) // also persent in AutoLanding TODO: move to common place
        {
            // retuns angle difference between -180 to 180
            return (angle - baseAngle + 540) % 360 - 180;
        }

        public void showOnMap(GMapOverlay overlay)
        {
            // Convert points to PointLatLng
            List<PointLatLng> points = new List<PointLatLng>();
            foreach (var p in pointList)
            {
                points.Add(new PointLatLng(p.Lat, p.Lng));
            }

            // Create polygon
            GMapPolygon polygon = new GMapPolygon(points, "Base zone")
            {
                Stroke = new Pen(Color.Red, 2),
                Fill = new SolidBrush(Color.FromArgb(10, Color.Red))
            };

            // Add to the map
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                overlay.Polygons.Clear();
                overlay.Polygons.Add(polygon);

                map.Refresh();
            }));

            showLinesForConvexPointPlacement(overlay);
        }

        private void showLinesForConvexPointPlacement(GMapOverlay overlay)
        {
            int N = pointList.Count-1;
            if (N < 2)
                return;
            PointLatLngAlt distant_point;


            distant_point = pointList[0].newpos(pointList[1].GetBearing(pointList[0]), 5000);
            GMapRoute line1 = new GMapRoute("a")
            {
                Stroke = new Pen(Color.FromArgb(125, Color.Orange), 2)
            };
            line1.Points.Add(pointList[1]);
            line1.Points.Add(distant_point);

            distant_point = pointList[N].newpos(pointList[N-1].GetBearing(pointList[N]), 5000);
            GMapRoute line2 = new GMapRoute("a")
            {
                Stroke = new Pen(Color.FromArgb(125, Color.Orange), 2)
            };
            line2.Points.Add(pointList[N]);
            line2.Points.Add(distant_point);

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                overlay.Routes.Clear();
                overlay.Routes.Add(line1);
                overlay.Routes.Add(line2);

                map.Refresh();
            }));
        }

        public void fadeLines(GMapOverlay overlay)
        {
            if (overlay.Routes == null || overlay.Routes.Count <= 1) return;

            double elapsedMs = (DateTime.Now - timeOfLastBasePoint).TotalMilliseconds;
            double progress = elapsedMs / fadeAwayTime;

            if (progress >= 1.0)
            {
                // Clear routes
                if (overlay.Routes != null && overlay.Routes.Count != 0)
                {
                    MainV2.instance.BeginInvoke((MethodInvoker)(() =>
                    {
                        // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                        var map = MainV2.instance.FlightData.gMapControl1;
                        if (map == null) return;

                        overlay.Routes.Clear();

                        map.Refresh();
                    }));    
                }
                    
                return;
            }

            int alpha = Convert.ToInt32(255 * (1.0 - progress));

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                overlay.Routes[0].Stroke = new Pen(Color.FromArgb(alpha, Color.Orange), 2);
                overlay.Routes[1].Stroke = new Pen(Color.FromArgb(alpha, Color.Orange), 2);

                map.Refresh();
            }));

        }
    }

    public class AutoLanding
    {
        #region Parameters
        // test
        public double testDist = 0.0;

        //--------------------------------
        // UAV parameters (for testing pourposes not assigned as const)
        //--------------------------------
        private float CHUTE_OPEN_TIME = 1.8f;                 // sec 
        private float WIND_DRAG = 1.8f;                       // - 
        private float SINK_RATE = 5f;                         // m/s 
        private double MAX_BANK_ANGLE = 30 * (Math.PI / 180); // rad 
        private float DECELERATION = 0.5f;                    // m/s^2

        //--------------------------------
        // Operator defined params (modified by AutoLandTester)
        //--------------------------------
        // Main
        // Land altitude

        // Override
        public bool overrideApprroach = false;
        public int overrideApproachDir = 0;        // degrees
        public int overrideApproachDistance = 100; // meters
        public Color displayColor = Color.White;
        public bool forceNextStage = false; // Forces the landing process to go to the next stage when the button is clicked, skipping the checks for the current stage completion

        //--------------------------------
        // Maneuver parameters
        //-------------------------------- 
        public TurnDirection turnDirection = TurnDirection.counterClockwise;
        private int landingAltitude = 100;          // meters
        private const int FLY_OVER_DISTANCE = 2000; // meters
        private double minBaseSafetyAngle = 10;     // degrees
        DateTime measurementStarted = DateTime.MinValue;
        public TimeSpan maxMeasurementTime = TimeSpan.FromSeconds(10);
        public LandingState state = LandingState.None;

        //--------------------------------
        // Airspeed values
        //-------------------------------- 
        // Most fuel efficient airspeed value
        public int cruiseAirSpeed = 50;      // m/s
        // Optimal airspeed to measure wind data
        public int measurementAirSpeed = 45; // m/s 
        // Minimum speed for safe loitering
        public int lineupAirSpeed = 35;      // m/s
        // Safe speed to open the parachute
        public int approachAirSpeed = 30;    // m/s 

        //--------------------------------
        // Threshold values
        //-------------------------------- 
        private int distanceThreshold = 20;    //meters
        private int speedThreshold = 2;        // m/s
        private int targetThreshold = 20;      // meters
        private float directionThreshold = 10; // deg

        //--------------------------------
        // Chute parameters // TODO get them from config.xml
        //-------------------------------- 
        public int chuteServo = Settings.Instance.GetInt32("chuteServo", 9);
        public int chuteServoOpenPWM = Settings.Instance.GetInt32("chuteServoOpenPWM", 1100);
        public bool chuteEnabled = false;

        //--------------------------------
        // Points of the landing sequence
        //--------------------------------
        // Desired landing location and the center of the wind measurement circles
        public PointLatLngAlt LandingPoint = new PointLatLngAlt();
        public bool isLandingSet = false;
        // Entry to the wind measurement circle
        public PointLatLngAlt EntryPoint = new PointLatLngAlt();
        // Exit point from the wind measurement circles, initiating lineup for landing
        public PointLatLngAlt TransitionPoint = new PointLatLngAlt();
        // Point where the UAV starts to performs the turning maneuver for final approach
        public PointLatLngAlt BankPoint = new PointLatLngAlt();
        // Point which around the UAV performs the turning maneuver for final approach
        public PointLatLngAlt TurnPoint = new PointLatLngAlt();
        // Starting point of the final approach to landing
        public PointLatLngAlt ApproachPoint = new PointLatLngAlt();
        // Target point to fly over the landing point
        public PointLatLngAlt FlyOverPoint = new PointLatLngAlt();

        // Direction from turn point to approach point 
        private float dirFromTurnToApproach = new float();
        // Direction from landing point to transition point 
        private float dirFromLandingToTransition = new float();
        // Distance from transition point to turn point
        private float distFromTransitionToTurn = new float();

        //--------------------------------
        // Dynamic parameters
        //-------------------------------- 
        // Based on the measurement airspeed and max bank angle calculate the loitering radius
        private int loiterRadius()
        {
            return (int)(Math.Pow(measurementAirSpeed, 2) / (Math.Tan(MAX_BANK_ANGLE) * 9.81)); // g = 9.81 m/s^2
        }
        // Based on the approach airspeed and chute open time calculate the minimum approach distance
        private int minApproachDistance() // meters
        {
            int openDistance = Convert.ToInt32(CHUTE_OPEN_TIME * approachAirSpeed);

            int decelerationDistance = Convert.ToInt32(Math.Abs(Math.Pow(lineupAirSpeed, 2) - Math.Pow(approachAirSpeed, 2)) / (2 * DECELERATION));

            return openDistance + decelerationDistance;
        }

        private (double min, double max) baseSafetyBearingMinMax()
        {
            double bearing_fix = LandingPoint.GetBearing(BaseZone.Instance.pointList[0]);
            double minDiff = 0;
            double maxDiff = 0;
            double minBearing = bearing_fix;
            double maxBearing = bearing_fix;

            for (int i = 1; i < BaseZone.Instance.pointList.Count; i++)
            {
                double beraing = LandingPoint.GetBearing(BaseZone.Instance.pointList[i]);

                double diff = angleDifference((float)beraing, (float)bearing_fix);

                if (diff < minDiff)
                {
                    minDiff = diff;
                    minBearing = beraing;
                }
                else if (diff > maxDiff)
                {
                    maxDiff = diff;
                    maxBearing = beraing;
                }
            }

            List<PointLatLng> points = new List<PointLatLng>
            {
                LandingPoint,
                LandingPoint.newpos(maxBearing, 2000),
                LandingPoint.newpos(minBearing, 2000),
            };
            GMapPolygon safeZone = new GMapPolygon(points, "_")
            {
                Stroke = new Pen(Color.Red, 0),
                Fill = new SolidBrush(Color.FromArgb(10, Color.Red))
            };
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
                    return;
                overlay.Polygons.Add(safeZone);
                map.Refresh();
            }));


            return (minBearing, maxBearing);
        }
        #endregion

        //--------------------------------
        // Constructor
        //--------------------------------
        private MAVLinkInterface port;
        public AutoLanding(MAVLinkInterface port)
        {
            this.port = port;
            // TODO: validate MAV port
        }

        #region Landing algorithm
        //--------------------------------
        // Main functions
        //--------------------------------
        public void startLandingProcess()
        {
            if (LandingPoint == new PointLatLngAlt())
                return;

            // Increment state
            state = LandingState.UpToRTLAltitude;
            forceNextStage = false;

            // Set cruise airspeed 
            if (!tryToChangeAirSpeed(cruiseAirSpeed))
            {
                // Handle error
                state = LandingState.Error;
                CustomMessageBox.Show("Unable to set speed", "Error");
                LandingLogger.GetInstance().Error($"Unable to set cruising speed", port.sysidcurrent);
            }
            else
            {
                LandingLogger.GetInstance().Info($"Speed set to cruising speed:  {cruiseAirSpeed} m/s", port.sysidcurrent);
            }

            // In current position fly up to RTL altitude
            if (!tryToSendToWayPoint(port.MAV.cs.Location, (float)port.MAV.param["RTL_ALTITUDE"].Value))
            {
                state = LandingState.Error;
                // Do not send to landing point if the plane cannot fly up to RTL altitude
                CustomMessageBox.Show("Unable to set waypoint", "ERROR");
                LandingLogger.GetInstance().Error($"Unable to set waypoint to climb to RTL", port.sysidcurrent);
            }
            else
            {
                LandingLogger.GetInstance().Info($"Climbing to RTL ALT", port.sysidcurrent);
            }

            // Draw wind measurement circle on the map
            drawWindMeasurementCircle();

            // If the landing use the override params do not wait with the drawing of the full maneuver 
            if (overrideApprroach)
            {
                setupLineupPoints();
                drawLineupToMap();
            }
        }

        public void abortLandingProcess()
        {
            // Increment state
            state = LandingState.None;

            // Set loiter radius to default value
            setLoiterRadius(loiterRadius());

            // Set cruise airspeed
            if (!tryToChangeAirSpeed(cruiseAirSpeed))
            {
                // Handle error
                CustomMessageBox.Show("Unable to set speed", "Error");
                LandingLogger.GetInstance().Error($"Unable to set cruising speed", port.sysidcurrent);
            }
            else
            {
                LandingLogger.GetInstance().Info($"Speed set to cruising speed:  {cruiseAirSpeed} m/s", port.sysidcurrent);
            }

            // Clear map drawings
            if (LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
            {
                MainV2.instance.BeginInvoke((MethodInvoker)(() =>
                {
                    // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                    var map = MainV2.instance.FlightData.gMapControl1;
                    if (map == null) return;
                    overlay.Markers.Clear();
                    overlay.Routes.Clear();
                    overlay.Polygons.Clear();
                    map.Refresh();
                }));
            }
            LandingLogger.GetInstance().Info($"Landing process aborted", port.sysidcurrent);
        }

        public void doLanding()
        {
            // If not connected or not armed, reset state.
            if (!port.MAV.cs.connected || !port.MAV.cs.armed)
            {
                state = LandingState.None;
                return;
            }

            // If not in landing mode, exit.
            if (state == LandingState.None) return;

            // Fly up to RTL altitude, then swicht state to head to the landing point.
            if (state == LandingState.UpToRTLAltitude)
            {
                // Check if we reached the RTL altitude
                if (port.MAV.cs.alt < (float)port.MAV.param["RTL_ALTITUDE"].Value && !forceNextStage) return;
                forceNextStage = false;

                // Increment state
                state = LandingState.GoToLandingPoint;

                // Determine entry point and loiter radius for the landing point approach
                // If the vehicle is close to the landing point, set it as the target.
                if (distToTarget(port.MAV.cs.Location, LandingPoint) <= 2 * loiterRadius())
                {
                    EntryPoint = LandingPoint;
                    // Set the loiter radius to measurement radius
                    setLoiterRadius(loiterRadius());
                }
                // Calculate the entry point to enable a smooth transition into the circle.
                else
                {
                    EntryPoint = LandingPoint.newpos(port.MAV.cs.Location.GetBearing(LandingPoint) - 90 * (int)turnDirection, loiterRadius());
                    // Set the loiter radius to 0 to get close to the enty point
                    setLoiterRadius(0);
                }

                // Send to entry point
                if (!tryToSendToWayPoint(EntryPoint, (float)port.MAV.param["RTL_ALTITUDE"].Value))
                {
                    state = LandingState.None;
                    CustomMessageBox.Show("Unable to go to landing point");
                    LandingLogger.GetInstance().Error($"Unable to set waypoint to landing point", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Going to landing point: {LandingPoint}", port.sysidcurrent);
                }
            }

            // Fly to the landing point, then switch to prepare for measurement.
            if (state == LandingState.GoToLandingPoint)
            {
                // Check if we reached the landing point
                if ((distToTarget(port.MAV.cs.Location, EntryPoint) >= (loiterRadius() + distanceThreshold)) && !forceNextStage) return;
                forceNextStage = false;

                // Increment state
                state = LandingState.PrepareForMeasurement;

                // Set the loiter radius for predefined value for the landing process
                setLoiterRadius(loiterRadius());

                // Set speed to measuring speed
                if (!tryToChangeAirSpeed(measurementAirSpeed))
                {
                    // Handle error
                    state = LandingState.Error;
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to set speed", "Error");
                    LandingLogger.GetInstance().Error($"Unable to set speed to measurement speed: {measurementAirSpeed} m/s", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Speed set to measurement speed: {measurementAirSpeed} m/s", port.sysidcurrent);
                }

                // Fly down to landing altitude
                if (!tryToSendToWayPoint(LandingPoint, landingAltitude))
                {
                    state = LandingState.Error;
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to set waypoint", "ERROR");
                    LandingLogger.GetInstance().Error($"Unable to set waypoint to landing point at landing altitude: {landingAltitude} m", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Descending to landing altitude: {landingAltitude} m", port.sysidcurrent);
                }
            }

            // Get to an appropriate airspeed and altitude for measurement, while loitering around the landing point. Then switch to wind measurement.
            if (state == LandingState.PrepareForMeasurement)
            {
                // Check if we reached the landing altitude and measuring speed
                if ((Math.Abs(port.MAV.cs.alt - landingAltitude) > distanceThreshold ||
                    Math.Abs(port.MAV.cs.airspeed - measurementAirSpeed) > speedThreshold) &&
                    !forceNextStage) return;
                forceNextStage = false;

                // Increment state
                state = LandingState.MeasureWind;

                // Start measurement timer
                measurementStarted = DateTime.Now;

                LandingLogger.GetInstance().Info($"Starting wind measurement", port.sysidcurrent);
            }

            // Perform wind measurement until time limit is reached or wind data is good enough. Then calculate the lineup points for landing, and switch to go to the lineup turn.
            if (state == LandingState.MeasureWind)
            {
                // Check if measurement time limit is reached. TODO: add wind data quality check
                if (DateTime.Now - measurementStarted <= maxMeasurementTime) 
                    return;
                forceNextStage = false;

                // Increment state
                state = LandingState.WaitForTransitionPoint;

                LandingLogger.GetInstance().Info($"Start lineup maneuver", port.sysidcurrent);

                setupLineupPoints();
                drawLineupToMap();
            }

            // Loiter around the landingpoint until the transition point is reached, then start the lineup maneuver.
            if (state == LandingState.WaitForTransitionPoint)
            {
                // Check if we reached the transition point
                float currentDir = Convert.ToSingle(LandingPoint.GetBearing(port.MAV.cs.Location));
                float dirDiff = Math.Abs(angleDifference(currentDir, dirFromLandingToTransition));
                if (dirDiff >= directionThreshold) 
                    return;

                // Increment state
                state = LandingState.GoToLineupCircle;

                // Set speed to lineup speed
                if (!tryToChangeAirSpeed(lineupAirSpeed))
                {
                    // Handle error
                    state = LandingState.Error;
                    CustomMessageBox.Show("Unable to set speed", "Error");
                    LandingLogger.GetInstance().Info($"Unable to set speed to lineup speed: {lineupAirSpeed} m/s", port.sysidcurrent);
                }
                else 
                {
                    LandingLogger.GetInstance().Info($"Speed set to lineupspeed: {lineupAirSpeed} m/s", port.sysidcurrent);
                }

                // Set radius to minimum to allow flying close to the bank point
                setLoiterRadius(1);

                // Send to bank point
                PointLatLngAlt OverShootPoint = TransitionPoint.newpos(TransitionPoint.GetBearing(BankPoint), BankPoint.GetDistance(TransitionPoint) + 500);
                if (!tryToSendToWayPoint(OverShootPoint, landingAltitude))
                {
                    state = LandingState.Error;
                    setLoiterRadius(loiterRadius());
                    tryToChangeAirSpeed(measurementAirSpeed);
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to fly to bank positon", "ERROR");
                    LandingLogger.GetInstance().Error($"Unable to set waypoint to lineup bank point: {BankPoint}", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Flying to lineup bank point: {BankPoint}", port.sysidcurrent);
                }
            }

            // Fly torwards the bank point, then start loitering around the turn point.
            if (state == LandingState.GoToLineupCircle)
            {
                // Check if we reached the bank point
                if (port.MAV.cs.Location.GetDistance(TransitionPoint) <= BankPoint.GetDistance(TransitionPoint))
                    return;

                // Increment state
                state = LandingState.LineupForLanding;

                // Set back loiter radius to predefined value
                setLoiterRadius(loiterRadius());

                // Start loitreing around the turn point
                if (!tryToSendToWayPoint(TurnPoint, landingAltitude))
                {
                    state = LandingState.Error;
                    tryToChangeAirSpeed(measurementAirSpeed);
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to set waypoint", "ERROR");
                }

                LandingLogger.GetInstance().Info($"Loitering around the turn point: {TurnPoint}", port.sysidcurrent);
            }

            // Loiter around the turn point to lieup for landing, then start the final approach.
            if (state == LandingState.LineupForLanding)
            {
                // Check if we reached the bank point
                float currentDir = Convert.ToSingle(TurnPoint.GetBearing(port.MAV.cs.Location));
                float dirDiff = Math.Abs(angleDifference(currentDir, dirFromTurnToApproach));
                if (dirDiff >= directionThreshold || 
                    ((Math.Abs(port.MAV.cs.airspeed - lineupAirSpeed) >= speedThreshold) && !forceNextStage))
                    return;
                forceNextStage = false;

                // Increment state
                state = LandingState.GoToLand;

                // Set speed to approach speed
                if (!tryToChangeAirSpeed(approachAirSpeed))
                {
                    // Handle error
                    state = LandingState.Error;
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to set speed", "Error");
                    LandingLogger.GetInstance().Error($"Unable to set speed to approach speed: {approachAirSpeed} m/s", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Speed set to approach speed: {approachAirSpeed} m/s", port.sysidcurrent);
                }

                // Send to flyover point
                if (!tryToSendToWayPoint(FlyOverPoint, landingAltitude))
                {
                    state = LandingState.Error;
                    tryToChangeAirSpeed(measurementAirSpeed);
                    tryToSendToWayPoint(LandingPoint, landingAltitude);
                    CustomMessageBox.Show("Unable to set waypoint", "ERROR");
                    LandingLogger.GetInstance().Error($"Unable to set waypoint to flyover point: {FlyOverPoint}", port.sysidcurrent);
                }
                else
                {
                    LandingLogger.GetInstance().Info($"Flying to flyover point: {FlyOverPoint}", port.sysidcurrent);
                }
            }

            // Approach to landing point, then open the parachute.
            if (state == LandingState.GoToLand)
            {
                PointLatLngAlt TouchDownPoint = predictTouchDownPoint();
                showTuchDown(TouchDownPoint);

                double distToLanding = distToTarget(TouchDownPoint, LandingPoint);

                LandingLogger.GetInstance().Info($"Predicted touchdown point: {TouchDownPoint}, distance to landing point: {distToLanding} m", port.sysidcurrent);

                if (distToLanding >= targetThreshold)
                {
                    // Check if the tuchdown point passed the landing zone
                    if (distToTarget(TouchDownPoint, ApproachPoint) >= getApproachDistance()+distanceThreshold)
                    {
                        // Set error state
                        state = LandingState.Error;
                        tryToChangeAirSpeed(measurementAirSpeed);
                        tryToSendToWayPoint(LandingPoint, landingAltitude);
                        CustomMessageBox.Show("Missed the landing zone.", "ERROR");
                        LandingLogger.GetInstance().Error($"Missed the landing zone.", port.sysidcurrent);
                    }

                    return;
                }

                // Increment State
                state = LandingState.Landed;

                // Open chute
                if (chuteEnabled)
                {
                    port.MAV.cs.messageHigh = "OPEN OPEN OPEN";
                    port.doCommand((byte)port.sysidcurrent, (byte)port.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, chuteServo, chuteServoOpenPWM, 0, 0, 0, 0, 0);
                    SystemSounds.Exclamation.Play();
                    LandingLogger.GetInstance().Info($"Chute opened", port.sysidcurrent);
                }
                else
                {
                    port.MAV.cs.messageHigh = "SIMULTED CHUTE OPEN";
                    SystemSounds.Exclamation.Play();
                    LandingLogger.GetInstance().Info($"Simulated chute opening", port.sysidcurrent);
                }
            }
        }

        //--------------------------------
        // Utility functions
        //--------------------------------
        private bool tryToSendToWayPoint(PointLatLngAlt waypoint, float altitude)
        {
            Locationwp gotohere = new Locationwp()
            {
                id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                alt = altitude,
                lat = (waypoint.Lat),
                lng = (waypoint.Lng)
            };

            try
            {
                port.setGuidedModeWP((byte)port.sysidcurrent, (byte)port.compidcurrent, gotohere);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool tryToChangeAirSpeed(int airSpeed)
        {
            try
            {
                // Speed can only change in GUIDED mode
                if (port.MAV.cs.mode != "Guided")
                {
                    // Try to set mode to GUIDED
                    port.setMode("Guided");
                }

                port.doCommandAsync((byte)port.sysidcurrent, (byte)port.compidcurrent,
                                    MAVLink.MAV_CMD.DO_CHANGE_SPEED, 
                                    0, (float)airSpeed, 0, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void setLoiterRadius(int radius)
        {
            // ATTENTION: the user is able to modify this from the ACTION tab (TODO: prevent this)

            // Set sing of radius based on turn direction
            radius = radius * (int)turnDirection;

            try
            {
                port.setParam(new[] { "WP_LOITER_RAD" }, radius);
            }
            catch
            {
                state = LandingState.None;
                CustomMessageBox.Show("Loiter radius can not be set.");
            }
        }

        private void setupLineupPoints()
        {
            // Setup approach point params
            float approachDir = getApproachDirection();
            int approachDistance = getApproachDistance();
             
            // Calculate points of the maneuver
            ApproachPoint = LandingPoint.newpos(approachDir - 180, approachDistance);
            TurnPoint = ApproachPoint.newpos(approachDir + 90 * (int)turnDirection, loiterRadius());
            FlyOverPoint = LandingPoint.newpos(approachDir, FLY_OVER_DISTANCE);

            double bearingToTurnPoint = LandingPoint.GetBearing(TurnPoint);
            TransitionPoint = LandingPoint.newpos(bearingToTurnPoint - 90 * (int)turnDirection, loiterRadius());
            BankPoint = TurnPoint.newpos(bearingToTurnPoint - 90 * (int)turnDirection, loiterRadius());

            // Get bearings and distance for increment checks in state machine
            dirFromTurnToApproach = Convert.ToSingle(TurnPoint.GetBearing(ApproachPoint));
            dirFromLandingToTransition = Convert.ToSingle(LandingPoint.GetBearing(TransitionPoint));
            distFromTransitionToTurn = Convert.ToSingle(TransitionPoint.GetDistance(TurnPoint));
        }

        private float getApproachDirection()
        {
            // In case of override, return the override value
            if (overrideApprroach)
                return overrideApproachDir;

            // Get wind direction
            float approachDir = port.MAV.cs.wind_dir;

            // If base point is not set, return wind direction as approach direction
            if (!BaseZone.Instance.isSet)
                return approachDir;

            // If base zone has less than 2 points, cannot calculate safety zone
            if (BaseZone.Instance.pointList.Count < 2)
                return approachDir;

            var bearingLimits = baseSafetyBearingMinMax();
            if (bearingLimits.min < bearingLimits.max) // eg min = 10deg and max = 30deg
            {
                // If approach dirrection is outside of safety zone
                if (bearingLimits.min >= approachDir || approachDir >= bearingLimits.max)
                {
                    return approachDir;
                }

                float diff1 = Math.Abs(angleDifference((float)bearingLimits.min, approachDir));
                float diff2 = Math.Abs(angleDifference((float)bearingLimits.max, approachDir));

                // return the dirrection closest to the desired
                if (diff1 > diff2)
                {
                    return (float)bearingLimits.max;
                }
                return (float)bearingLimits.min;
            }
            else // eg min = 350deg and max = 30deg
            {
                {
                    // If approach dirrection is outside of safety zone
                    if (bearingLimits.min <= approachDir && approachDir >= bearingLimits.max)
                    {
                        return approachDir;
                    }

                    float diff1 = Math.Abs(angleDifference((float)bearingLimits.min, approachDir));
                    float diff2 = Math.Abs(angleDifference((float)bearingLimits.max, approachDir));

                    // return the dirrection closest to the desired
                    if (diff1 > diff2)
                    {
                        return (float)bearingLimits.max;
                    }
                    return (float)bearingLimits.min;
                }

                return approachDir;
            }
        }

        private int getApproachDistance()
        {
            // In case of override, set approach distance to override value if able, otherwise use minimum distance
            if (overrideApprroach)
                return Math.Max(overrideApproachDistance, minApproachDistance());

            return minApproachDistance();
        }   

        private PointLatLngAlt predictTouchDownPoint()
        {
            // Calculate touchdown point
            PointLatLngAlt CurrentPosition = new PointLatLngAlt(port.MAV.cs.Location);
            PointLatLngAlt OpenPoint = CurrentPosition.newpos(port.MAV.cs.yaw, port.MAV.cs.airspeed * CHUTE_OPEN_TIME);
            PointLatLngAlt TouchDownPoint = OpenPoint.newpos(port.MAV.cs.wind_dir-180, (port.MAV.cs.wind_vel * (port.MAV.cs.alt / SINK_RATE) * WIND_DRAG));

            return TouchDownPoint;
        }

        private double distToTarget(PointLatLngAlt pos, PointLatLngAlt target)
        {
            double dist = pos.GetDistance(target);
            testDist = dist;

            return dist;
        }

        private float angleDifference(float angle, float baseAngle) 
        {
            // retuns angle difference between -180 to 180
            return (angle - baseAngle + 540) % 360 - 180;
        }

        //--------------------------------
        // Utility functions (drawing)
        //--------------------------------
        // Connect colors to GMarkerTypes, since GMarkerGoogle can only change its type, not its color.
        Dictionary<Color, GMarkerGoogleType> colorToMarkerType = new Dictionary<Color, GMarkerGoogleType>
                                            {
                                                { Color.White, GMarkerGoogleType.white_small },
                                                { Color.Orange, GMarkerGoogleType.orange_small },
                                                { Color.Red, GMarkerGoogleType.red_small },
                                                { Color.Blue, GMarkerGoogleType.blue_small }
                                            };
        public GMarkerGoogleType getMarkerType()
        {
            return colorToMarkerType[displayColor];
        }

        private void drawWindMeasurementCircle()
        {
            // Create circle
            GMapPolygon MeasurementCircle = genCircle(LandingPoint, loiterRadius(), displayColor);

            // Add everything to the map
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
                    return;

                // Add circle
                overlay.Polygons.Add(MeasurementCircle);

                map.Refresh();
            }));
        }

        private void drawLineupToMap()
        {
            // Create circle
            GMapPolygon TurningCircle = genCircle(TurnPoint, loiterRadius(), displayColor);

            // Create lines
            GMapRoute lineupRoute = new GMapRoute("lineupline")
            {
                Stroke = new Pen(displayColor, 2)
            };
            lineupRoute.Points.Add(TransitionPoint);
            lineupRoute.Points.Add(BankPoint);

            GMapRoute approachRoute = new GMapRoute("approachline")
            {
                Stroke = new Pen(displayColor, 2)
            };
            approachRoute.Points.Add(ApproachPoint);
            approachRoute.Points.Add(FlyOverPoint);

            // Add everything to the map
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
                    return;

                // Add circle
                overlay.Polygons.Add(TurningCircle);

                // Add lines
                overlay.Routes.Add(lineupRoute);
                overlay.Routes.Add(approachRoute);

                map.Refresh();
            }));
        }

        private void showTuchDown(PointLatLngAlt TouchDownPoint)
        {
            GMarkerGoogle touchDownMarker = new GMarkerGoogle(TouchDownPoint, GMarkerGoogleType.yellow_small);
            touchDownMarker.Tag = "Touch down";


            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
                    return;
                try
                {
                    overlay.Markers.RemoveAt(1);
                }
                catch { }
                overlay.Markers.Add(touchDownMarker);
                map.Refresh();
            }));
        }

        private GMapPolygon genCircle(PointLatLngAlt point, int radius, Color color)
        {
            int SEGMENTS = 36; // More segments = smoother circle

            // Calc points for circle
            List<PointLatLng> points = new List<PointLatLng>();
            for (int i = 0; i < SEGMENTS; i++)
            {
                double angle = i * (360.0 / SEGMENTS);
                double dx = radius * Math.Cos(angle * Math.PI / 180);
                double dy = radius * Math.Sin(angle * Math.PI / 180);

                // Convert meters to lat/lng offset
                double latOffset = dy / 111320.0;
                double lngOffset = dx / (111320.0 * Math.Cos(point.Lat * Math.PI / 180));

                points.Add(new PointLatLng(point.Lat + latOffset, point.Lng + lngOffset));
            }

            // TODO: circle method (in built)

            // Create polygon
            GMapPolygon circle = new GMapPolygon(points, "landingCircle")
            {
                Stroke = new Pen(color, 2),
                Fill = Brushes.Transparent //new SolidBrush(Color.FromArgb(0, Color.White))
            };

            return circle;
        }

        public void updateDrawingColor()
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Use MainV2.instance.FlightData.gMapControl1 (map control) because 'Host' is not available here.
                var map = MainV2.instance.FlightData.gMapControl1;
                if (map == null) return;

                // Get the overlay for this plane
                if (!LandingHandler.Instance.landingPointOverlays.TryGetValue(port.sysidcurrent, out var overlay))
                    return;

                // Update colors
                foreach (GMapPolygon circle in overlay.Polygons)
                { 
                    circle.Stroke.Color = displayColor;
                }

                foreach (GMapRoute line in overlay.Routes)
                {
                    line.Stroke.Color = displayColor;
                }

                // Remove each marker and replace with new color, keeping text
                var markersToReplace = overlay.Markers.ToList();
                overlay.Markers.Clear();

                foreach (var oldMarker in markersToReplace)
                {
                    // Get marker type for the selected color
                    GMarkerGoogleType markerType;
                    if (!colorToMarkerType.TryGetValue(displayColor, out markerType))
                    {
                        markerType = GMarkerGoogleType.white_small; // fallback type
                    }

                    // Create new marker with new color/type and preserved text
                    var newMarker = new GMarkerGoogle(oldMarker.Position, markerType)
                    {
                        ToolTipText = oldMarker.ToolTipText,
                        ToolTipMode = oldMarker.ToolTipMode,
                        Tag = oldMarker.Tag
                    };

                    overlay.Markers.Add(newMarker);
                }

                map.Refresh();
            }));
        }
        #endregion
    }
    #endregion
}

