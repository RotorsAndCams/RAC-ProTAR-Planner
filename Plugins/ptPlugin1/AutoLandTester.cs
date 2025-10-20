using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ptPlugin1
{
    #region UI
    public partial class AutoLandTester : UserControl
    {
        public AutoLandTester()
        {
            InitializeComponent();
        }
    }
    #endregion

    #region Handler
    public class AutoLandHandler
    {
        //--------------------------------
        // UAV parameters (for testing pourposes not assigned as const)
        //--------------------------------
        private float CHUTE_OPEN_TIME = 1.8f;                 // sec 
        private float WIND_DRAG = 1.8f;                       // - 
        private float SINK_RATE = 5f;                         // m/s 
        private double MAX_BANK_ANGLE = 30 * (Math.PI / 180); // rad 
        private float DECELERATION = 2f;                      // m/s^2

        //--------------------------------
        // Maneuver parameters
        //-------------------------------- 
        private int loiterRadius = 300;     // meters
        private int landingAltitude = 100;  // meters
        private const int FLY_OVER_DISTANCE = 2000; // meters

        //--------------------------------
        // Airspeed values
        //-------------------------------- 
        private int lineupAirSpeed = 35;   // m/s
        private int approachAirSpeed = 30; // m/s 

        //--------------------------------
        // Points of the landing sequence
        //--------------------------------
        // The position of the operators
        public PointLatLngAlt BasePoint = new PointLatLngAlt();
        // Desired landing location and the center of the wind measurement circles
        public PointLatLngAlt LandingPoint = new PointLatLngAlt();
        public bool isLandingSet = false;
        // Exit point from the wind measurement circles, initiating lineup for landing
        public PointLatLngAlt TransitionPoint = new PointLatLngAlt();
        // Point where the UAV begins the turn toward final approach
        public PointLatLngAlt BankPoint = new PointLatLngAlt();
        // Point where the UAV performs the turning maneuver for final approach
        public PointLatLngAlt TurnPoint = new PointLatLngAlt();
        // Starting point of the final approach to landing
        public PointLatLngAlt ApproachPoint = new PointLatLngAlt();

        //--------------------------------
        // Dynamic parameters
        //-------------------------------- 
        private int measuringSpeed() // m/s
        {
            return Convert.ToInt32(Math.Sqrt(Math.Tan(MAX_BANK_ANGLE) /
                                                   (9.81 * loiterRadius))); // g = 9.81 m/s^2
        }
        private int minApproachDistance() // meters
        {
            int openDistance = Convert.ToInt32(CHUTE_OPEN_TIME * approachAirSpeed);
            int decelerationDistance = Convert.ToInt32(Math.Abs(Math.Pow(lineupAirSpeed, 2) - Math.Pow(approachAirSpeed, 2)) / (2 * DECELERATION));

            return openDistance + decelerationDistance;
        }
    }
    #endregion
}

