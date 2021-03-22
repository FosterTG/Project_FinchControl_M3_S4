using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Authors: Velis, John / Gilmore, Foster
    // Dated Created: 1/22/2020 
    // Last Modified: 2/22/2021
    //
    // **************************************************

    public enum Command
    {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            GETTEMPRATURE,
            DONE,
    }

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgramingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }


        #region USER PROGRAMING

        static void UserProgramingDisplayMenuScreen(Finch finchRobot)
        {


            bool quitApplication = false;
            string menuChoice;


            // Tuple to store three command paramaters

            (int motorSpeed, int ledBrightness, double waiteSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waiteSeconds = 0;

            List<Command> commands = new List<Command>();

            


            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\te) Quit");
                
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgramDisplayGetCommandParamaters();
                        break;

                    case "b":
                        UserProgramingGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgramingDisplayFinchCommandsView(commands);
                        break;

                    case "d":
                        UserProgramingDisplayExecuteFinchCommands(finchRobot, commands ,commandParameters);
                        break;

                    case "e":
                        
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);


        }
        // execute user programming
        private static void UserProgramingDisplayExecuteFinchCommands(
            Finch finchRobot, 
            List<Command> commands, 
            (int motorSpeed, int ledBrightness, double waiteSeconds) commandParameters)
        {
            int motorspeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliseconds = (int)(commandParameters.waiteSeconds * 1000);
            string commandFeedBack = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayScreenHeader("Execute Finch Commands");

            Console.WriteLine("\tThe Finch robot is ready to execute the list of commands.");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;
                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorspeed, motorspeed);
                        commandFeedBack = Command.MOVEFORWARD.ToString();
                        break;
                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorspeed, -motorspeed);
                        commandFeedBack = Command.MOVEBACKWARD.ToString();
                        break;
                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedBack = Command.STOPMOTORS.ToString();
                        break;
                    case Command.WAIT:
                        finchRobot.wait(waitMilliseconds);
                        commandFeedBack = Command.WAIT.ToString();
                        break;
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedBack = Command.TURNRIGHT.ToString();
                        break;
                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedBack = Command.TURNLEFT.ToString();
                        break;
                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness,ledBrightness);
                        commandFeedBack = Command.LEDON.ToString();
                        break;
                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedBack = Command.LEDOFF.ToString();
                        break;
                    case Command.GETTEMPRATURE:
                        commandFeedBack = $"Temprature: {finchRobot.getTemperature().ToString("n2")}\n";
                        break;
                    case Command.DONE:
                        commandFeedBack = Command.DONE.ToString();
                        break;
                    default:
                        break;

                }
                Console.WriteLine($"\t{commandFeedBack}");
            }
        }

        // View Commands
        private static void UserProgramingDisplayFinchCommandsView(List<Command> commands)
        {
            DisplayScreenHeader("View Finch Commands");
            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }
            DisplayContinuePrompt();


        }
        // Enter Commands
        private static void UserProgramingGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;
            DisplayScreenHeader("Finch Robot Commands");

            // List  Commands__________________________________________________

            int commandCount = 1;
            Console.WriteLine("\tList of Available Commands");
            Console.WriteLine();
            Console.Write("\t-");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()}   -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }
            Console.WriteLine();
            //__________________________________________________________________
            while (command != Command.DONE)
            {
                Console.Write("\tEnter Command:");
                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t***********************************************");
                    Console.WriteLine("\t\t* Please Enter A Command From The List Above. *");
                    Console.WriteLine("\t\t***********************************************");
                }

            }
            DisplayContinuePrompt();
        }
        // Get Motor,Led,Wait Info
        static (int motorSpeed, int ledBrightness, double waiteSeconds) UserProgramDisplayGetCommandParamaters()
        {
            (int motorSpeed, int ledBrightness, double waiteSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waiteSeconds = 0;

            bool validResponse;
            int userIntInput;
            double userDoubleInput;

            DisplayScreenHeader("Command Parameters");

            //
            // Getting User input for Motor Speed______________________
            //
            do
            {

                Console.WriteLine("Enter Motor Speed between [1-255]:");
                Int32.TryParse(Console.ReadLine(),out userIntInput);
                if (userIntInput < 1 || userIntInput > 255)
                {
                    Console.WriteLine("Error, Enter valid number.");
                    validResponse = false;
                }
                else
                {
                    commandParameters.motorSpeed = userIntInput;
                    validResponse = true;
                }
            } while (!validResponse);
            //
            // Get User input for LED brightness____________________________
            //
            do
            {
                Console.WriteLine("Enter Led Brightness [1-255]:");
                Int32.TryParse(Console.ReadLine(), out userIntInput);
                if (userIntInput < 1 || userIntInput > 255)
                {
                    Console.WriteLine("Error, Enter valid number.");
                    validResponse = false;
                }
                else
                {
                    commandParameters.ledBrightness = userIntInput;
                    validResponse = true;
                }
            } while (!validResponse);
            //
            // Get User input for Wait time_________________________________
            //
            do
            {
                Console.WriteLine("Enter wait time in seconds [0-10]:");
                if (!double.TryParse(Console.ReadLine(), out userDoubleInput) || userDoubleInput < 0 || userDoubleInput > 10)
                {
                    Console.WriteLine("Error, Enter valid number.");
                    validResponse = false;
                }
                else 
                {
                    commandParameters.waiteSeconds = userDoubleInput;
                    validResponse = true;
                }
            } while (!validResponse);
            //

            Console.WriteLine("Motor Speed: "+commandParameters.motorSpeed);
            Console.WriteLine("LED Brightness: " + commandParameters.ledBrightness);
            Console.WriteLine("Wait Time in Seconds: " + commandParameters.waiteSeconds);

            DisplayContinuePrompt();

            return commandParameters; 
        }




        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound (Wind Up and Down");
                Console.WriteLine("\tb) Dance (Finch Dance)");
                Console.WriteLine("\tc) Mixing it Up (Drag Race)");
                Console.WriteLine("\td) Your Finch");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalantShowDisplayMixItUp(finchRobot);
                        break;

                    case "d":
                        
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// 

        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
            DisplayContinuePrompt();
            // Light wind up and down Red
            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, 0, 0);
                finchRobot.noteOn(lightSoundLevel * 20);
            }
            for (int lightSoundLevel = 255; lightSoundLevel > 1; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, 0, 0);
                finchRobot.noteOn(lightSoundLevel * 20);
            }
            // Light wind Up and Down Green
            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(0, lightSoundLevel, 0);
                finchRobot.noteOn(lightSoundLevel * 50);
            }
            for (int lightSoundLevel = 255; lightSoundLevel > 1; lightSoundLevel--)
            {
                finchRobot.setLED(0, lightSoundLevel, 0);
                finchRobot.noteOn(lightSoundLevel * 50);
            }
            // Light wind Up and Down Blue
            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(0, 0, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }
            for (int lightSoundLevel = 255; lightSoundLevel > 1; lightSoundLevel--)
            {
                finchRobot.setLED(0, 0, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }



            //Stop
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();
            DisplayMenuPrompt("Talent Show Menu");
        }

        static void TalentShowDisplayDance(Finch finchRobot)
        {
            
            /// *****************************************************************
            /// *               Talent Show > Dance                             *
            /// *****************************************************************
            /// </This method directs the finch to perform a dance>

            Console.CursorVisible = false;
            
            DisplayScreenHeader("Dance");

            Console.WriteLine("\tThe Finch robot will now show off its dance!");
            DisplayContinuePrompt();

            //Move Backwards 0.5s
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(500);

            //Move Forwards 0.5s
            finchRobot.setMotors(255, 255);
            finchRobot.wait(500);

            //Turn Left 1s
            finchRobot.setMotors(-30, 255);
            finchRobot.wait(1000);

            //Turn Right 1s
            finchRobot.setMotors(255, -30);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            //Move Forwards 0.5s
            finchRobot.setMotors(255, 255);
            finchRobot.wait(500);

            //Move Backwards 0.5s
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(500);

            
            //Stop
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            DisplayMenuPrompt("Talent Show Menu");
        }

        static void TalantShowDisplayMixItUp(Finch finchRobot)
        {
            /// *****************************************************************
            /// *               Talent Show > Mix it Up (Drag Race)             *
            /// *****************************************************************
            /// </This method directs the finch to perform a Dance, Light, and Sound in a drag race>

            Console.CursorVisible = false;

            DisplayScreenHeader("Mixing it Up");

            Console.WriteLine("\tThe Finch robot will now perform a drag race!");
            DisplayContinuePrompt();

            //Ready
            finchRobot.setLED(255, 0, 0);
            finchRobot.noteOn(1600);
            finchRobot.wait(700);
            finchRobot.noteOn(0);
            finchRobot.setLED(0, 0, 0);
            //Ready
            finchRobot.setLED(255, 0, 0);
            finchRobot.noteOn(1600);
            finchRobot.wait(700);
            finchRobot.noteOn(0);
            finchRobot.setLED(0, 0, 0);
            //Ready
            finchRobot.setLED(255, 200, 0);
            finchRobot.noteOn(1600);
            finchRobot.wait(700);
            finchRobot.noteOn(0);
            finchRobot.setLED(0, 0, 0);
            //GO
            finchRobot.setLED(0, 255, 0);
            finchRobot.noteOn(2500);
            finchRobot.wait(200);
            finchRobot.noteOn(0);
            finchRobot.setLED(0, 0, 0);
            
            //First Gear
            for (int acceleration = 30; acceleration < 85; acceleration++)
            {
                finchRobot.setMotors(acceleration, acceleration);
                
            }
            finchRobot.noteOn(2000);
            finchRobot.noteOn(0);
            //Second Gear
            for (int acceleration = 85; acceleration < 170; acceleration++)
            {
                finchRobot.setMotors(acceleration, acceleration);
                
            }
            finchRobot.noteOn(2000);
            finchRobot.noteOn(0);
            //Third Gear
            for (int acceleration = 170; acceleration < 255; acceleration++)
            {
                finchRobot.setMotors(acceleration, acceleration);
                
            }
            finchRobot.noteOn(1000);
            finchRobot.noteOn(0);
            // Drive On for 2s
            finchRobot.wait(2000);

            // Victory Spin sound and Light
            finchRobot.setMotors(-255, 255);
            finchRobot.wait(2000);

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel+=3)
            {
                finchRobot.setLED(lightSoundLevel, 30, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }
            finchRobot.setLED(0, 0, 0);

            //Stop
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();
            DisplayMenuPrompt("Talent Show Menu");
        }



        #endregion

        #region DATA RECOREDER

        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {

            int numberOfDataPoints = 0;
            double dataPointFrequancy = 0;
            double[] tempratures = null;

            Console.CursorVisible = true;

            bool quitDataRecorderMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of data points");
                Console.WriteLine("\tb) Frequancy of data points");
                Console.WriteLine("\tc) Get data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequancy = DataRecorderDisplayGetFrequancyOfDataPoints();
                        break;

                    case "c":
                        tempratures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequancy, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayData(tempratures, numberOfDataPoints, dataPointFrequancy);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);

        }

        static void DataRecorderDisplayData(double[] tempratures, int numberOfDataPoints, double dataPointFrequancy)
        {

            DisplayScreenHeader("SHow Data");
            //
            //display table haeaders

            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temprature".PadLeft(15));
            Console.WriteLine(
                "-------------".PadLeft(15) +
                "----------".PadLeft(15));

            //
            // display table data 

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                Console.WriteLine(
                (index + 1).ToString().PadLeft(15) +
                tempratures[index].ToString("n2").PadLeft(15));
            }

            DisplayContinuePrompt();

        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequancy, Finch finchRobot)
        {

            double[] tempratures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Data");

            Console.WriteLine($"Number of data points: {numberOfDataPoints}");
            Console.WriteLine($"Data point frequancy: {dataPointFrequancy}");
            Console.WriteLine();
            Console.WriteLine("\tThe finch Robot is ready to collect temprature data");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                tempratures[index] = finchRobot.getTemperature();
                Console.WriteLine($"\tReading {index + 1}: {tempratures[index].ToString("n2")}:");
                int waitInSeconds = (int)(dataPointFrequancy * 1000);
                finchRobot.wait(waitInSeconds);

            }


            DisplayContinuePrompt();
            return tempratures;
        }

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            /// <summary>
            /// Get Number of Data Points From the User. 
            /// </summary>
            /// <returns> Number of Data points </returns>
            /// 
            int numberOfDataPoints;
            string userResponse;
            bool validResponse = true;
            DisplayScreenHeader("Number of Data Points");

            do
            {
                Console.Write("\tEnter Number of Data Points: ");
                userResponse = Console.ReadLine();
                validResponse = int.TryParse(userResponse, out numberOfDataPoints);

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease enter a valid response.");
                }

            } while (!validResponse);

            DisplayContinuePrompt();
            return numberOfDataPoints;

        }

        static double DataRecorderDisplayGetFrequancyOfDataPoints()
        {

            /// <summary>
            /// Get Frequancy From the User. 
            /// </summary>
            /// <returns> Frequancy Set </returns>
            /// 
            double datapointFrequancy;
            string userResponse;
            bool validResponse = true;
            DisplayScreenHeader("Set Frequancy");

            do
            {
                Console.Write("\tEnter frequancy value: ");
                userResponse = Console.ReadLine();
                validResponse = double.TryParse(userResponse, out datapointFrequancy);

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease enter a valid response.");
                }

            } while (!validResponse);

            DisplayContinuePrompt();
            return datapointFrequancy;

        }



        #endregion

        #region ALARM SYSTEM
        static void LightAlarmDisplayMenuScreen(Finch finchRobot)
        {

            bool quitDataRecorderMenu = false;
            string menuChoice;

            string sensersToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor=0;


            do
            {
                DisplayScreenHeader("Light Alarm System");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Min/Max Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensersToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinMaxThresholdValue(finchRobot, rangeType);
                        break;

                    case "d":
                        timeToMonitor = LightAlarDisplaySetTimeToMonitor(); 
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensersToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }


            } while (!quitDataRecorderMenu);


        }

        static int LightAlarDisplaySetTimeToMonitor()
        {

            int timeToMonitor;
            string userResponse;

            DisplayScreenHeader("Set Time to Monitor");
            Console.WriteLine($"\tSet time to monitor to:");

            userResponse = Console.ReadLine();
            int.TryParse(userResponse, out timeToMonitor);


            Console.ReadKey();


            DisplayMenuPrompt("Light Alarm");
            return timeToMonitor;
        }

        static void LightAlarmSetAlarm(Finch finchRobot,string sensersToMonitor,string rangeType,int minMaxThresholdValue,int timeToMonitor)
           
        {

            int secondselapised = 0;
            bool thresholdexited = false;
            int currentSensorValue =0;
            DisplayScreenHeader("Set Alarm");


            Console.WriteLine($"\tSensors to monitor {sensersToMonitor}");
            Console.WriteLine($"\tRange type {rangeType}");
            Console.WriteLine($"\tMin/max threshold value {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to monitor {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondselapised < timeToMonitor) && !thresholdexited)
            {
                switch (sensersToMonitor)
                {
                    case "left":
                        currentSensorValue = finchRobot.getLeftLightSensor();
                        break;
                    case "right":
                        currentSensorValue = finchRobot.getRightLightSensor();
                        break;
                    case "both":
                        currentSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;
                }


                switch (rangeType)
                {

                    case "minimum":
                        if (currentSensorValue < minMaxThresholdValue)
                        {
                            thresholdexited = true;
                        }


                        break;
                    case "maximum":
                        if (currentSensorValue > minMaxThresholdValue)
                        {
                            thresholdexited = true;
                        }

                        break;
                }

                secondselapised++;


            }

            if (thresholdexited)
            {
                Console.WriteLine($"The {rangeType} threshld value of {minMaxThresholdValue} was exceded by the \ncurrent light sensor value of {currentSensorValue}.");
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshld value of {minMaxThresholdValue} was not exceded \nby the current light sensor value of {currentSensorValue}.");

            }


            DisplayMenuPrompt("Light Alarm");

        }
        

        static int LightAlarmDisplaySetMinMaxThresholdValue(Finch finchRobot, string rangeType)
        {

            
            int minMaxThresholdValue=0;

            DisplayScreenHeader("Set Min/Max Threshold Value");


            
            Console.WriteLine($"\tLeft light sensor ambient value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tRight light sensor ambient value: {finchRobot.getRightLightSensor()}");
            Console.WriteLine();

            Console.WriteLine("\tEnter the" + rangeType +"light sensor value:");
            int.TryParse(Console.ReadLine(),out minMaxThresholdValue);

            // Validate and echo Values--------------------------------------------------------------------------------



            DisplayMenuPrompt("Light Alarm");
            return minMaxThresholdValue;

        }
        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;

            DisplayScreenHeader("Set Sensors To Monitor");

            
            

            Console.WriteLine("\tSensores to Monitor [left, Right, Both]");
            sensorsToMonitor = Console.ReadLine();
            // validate


            
            DisplayMenuPrompt("Light Alarm");

            return sensorsToMonitor; 

        }
        static string LightAlarmDisplaySetRangeType()
        {
            string sensorsToMonitor;

            DisplayScreenHeader("Set Range Type");


            //validate

            Console.WriteLine("\tRange Type [Minimum, Maximum]:");
            sensorsToMonitor = Console.ReadLine();




            DisplayMenuPrompt("Light Alarm");

            return sensorsToMonitor;

        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
