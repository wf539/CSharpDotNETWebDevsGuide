using System;
using System.Collections;
using System.IO;

/// <summary>
/// Contains the entry point for the Payroll sample
/// </summary>
class PayrollSample
{
   static void Main( string[] args )
   {
      try
      {
         // Create a container to hold employees
         Employees employees = new Employees();

         // Create and drain our simulated message queue
         EmployeeQueueMonitor monitor = 
            new EmployeeQueueMonitor( employees );
            
         monitor.start();
         monitor.stop();   

         // Display employees that were added and their payroll         
         Console.WriteLine( "List of employees added via event:" );
         foreach ( Employee employee in employees )
         {
            string name = employee.FirstName + " " + 
               employee.MiddleName + " " + employee.LastName;

            string ssn = employee.SSN;
            string empType = employee.GetType().ToString();

            Console.WriteLine( 
               "Name: {0}, SSN: {1}, Pay Type: {2}, Pay: {3}", 
               name, ssn, empType, employee.Payroll.ToString() );    
         }
      }
      catch ( Exception exception )
      {
         // Write exceptions to screen
         Console.WriteLine( exception.Message );
      }
   }
}


/// <summary>
/// Defines the data that will be passed from the delegate to the
/// the callback method when the event is raised, in this case
/// the employee information.
/// </summary>
class AddEmployeEventArgs : EventArgs
{
   int m_ID;
   string m_FirstName; 
   string m_LastName;
   string m_MiddleName;
   string m_SSN;
   bool m_IsSalaried;
   double m_PayRate;

   public AddEmployeEventArgs( AddEmployeeMessage msg )
   {
      m_ID = msg.ID;
      m_FirstName = msg.FirstName;
      m_LastName = msg.LastName;
      m_MiddleName = msg.MiddleName;
      m_SSN = msg.SSN;
      m_IsSalaried = msg.IsSalaried;
      m_PayRate = msg.PayRate;
   } 
   
   // Event argument properties contain the data to pass to the
   // callback methods subscribed to the event.
   public int ID            { get { return m_ID; } }
   public string FirstName  { get { return m_FirstName; } }
   public string LastName   { get { return m_LastName; } }
   public string MiddleName { get { return m_MiddleName; } }
   public string SSN        { get { return m_SSN; } }
   public bool IsSalaried   { get { return m_IsSalaried; } }
   public double PayRate    { get { return m_PayRate; } }
}


/// <summary>
/// Defines the data that will be passed from the delegate to the
/// the callback method when the event is raised, in this case
/// the hourse worked information.
/// </summary>
class HoursWorkedEventArgs : EventArgs
{
   int m_ID;
   double m_Hours; 

   public HoursWorkedEventArgs( PayrollMessage msg ) 
   {
      m_ID = msg.ID;
      m_Hours = msg.Hours;
   } 
   
   // Event argument properties contain the data to pass to the
   // callback methods subscribed to the event.
   public int ID       { get { return m_ID; } }
   public double Hours { get { return m_Hours; } }
}


/// <summary>
/// Simulates monitoring a message queue. When an message appears in 
/// the the event is raised and methods subscribed to the event
//  are invoked.
/// </summary>
class EmployeeQueueMonitor
{
   // Event signature for AddEmployeeEvent
   public delegate void AddEmployeeEvent( object sender,
      AddEmployeEventArgs e ); 

   // Event signature for HoursWorkedEvent
   public delegate void HoursWorkedEvent( object sender,
      HoursWorkedEventArgs e ); 
      
   // Instance of each event 
   public event AddEmployeeEvent OnAddEmployee;
   public event HoursWorkedEvent OnHoursWorked;
   
   private EmployeeQueueLogger m_logger;
   private ErrorLogger m_errLogger;
   
   private Employees m_employees;
   private EmployeeMessageQueue m_queue;
   
   public EmployeeQueueMonitor( Employees employees )
   {
      m_employees = employees;
      m_queue = new EmployeeMessageQueue();

      m_logger = new EmployeeQueueLogger( "log.txt" );
      m_errLogger = new ErrorLogger( "errlog.txt" );
      
      // Register the event callbacks
      OnAddEmployee += 
         new AddEmployeeEvent( this.addEmployee );
      OnAddEmployee += 
         new AddEmployeeEvent( m_logger.logAddRequest );

      OnHoursWorked += 
         new HoursWorkedEvent( this.addHoursWorked );
      OnHoursWorked += 
         new HoursWorkedEvent( m_logger.logHoursWorked );
   }

   // Drain the queue.
   public void start()
   {
      if ( m_employees == null )
         return;

      // Process all queue messages.
      while ( m_queue.moreMessages() == true )
      {
         try
         {
            EmployeeMessage msg = m_queue.getNextMessage();
            if ( msg.MessageType == MsgType.Add )
            {
               AddEmployeEventArgs args = new AddEmployeEventArgs( 
                  (AddEmployeeMessage)msg ); 
               OnAddEmployee( this, args );
            }
            else
            {
               HoursWorkedEventArgs args = new HoursWorkedEventArgs(  
                  (PayrollMessage) msg );
               OnHoursWorked( this, args );
            }   
         }
         catch ( Exception e )
         {
            m_errLogger.logError( e );
         }
      }
      
      // Now read one more message to see our custom exception
      // thrown
      try
      {
         EmployeeMessage msg = m_queue.getNextMessage();
      }
      catch ( Exception e )
      {
            m_errLogger.logError( e );
      }
      
   }
      
   public void stop()
   {
      // In a real communications program you would shut down
      // gracefully.
   }
   
   // Called by event whenever a new add employee message appears
   // in the message queue. Notice the signature matches that requried
   // by System.Event   
   public void addEmployee( object sender, AddEmployeEventArgs e )
   {
      Console.WriteLine( "In delegate, adding employee\r\n" );

      Employee employee;
      if ( e.IsSalaried == true )
      {
         employee = new SalariedEmployee ( e.ID, e.FirstName, e.MiddleName, 
            e.LastName, e.SSN, e.PayRate );
      }
      else
      {
         employee = new HourlyEmployee ( e.ID, e.FirstName, e.MiddleName, 
            e.LastName, e.SSN, e.PayRate );
      }
      
      m_employees.Add( employee );
   }

   // Called by event whenever a new add hours worked message 
   // appears in the message queue. Notice the signature matches
   // that requried by System.Event   
   public void addHoursWorked( object sender, HoursWorkedEventArgs e )
   {
      Console.WriteLine( "In delegate, adding employee hours\r\n" );
      foreach ( Employee employee in m_employees )
      {
         if ( employee.ID == e.ID )
         {
            if ( employee.GetType().ToString() == "HourlyEmployee" )
            {
               HourlyEmployee hourlyEmp = (HourlyEmployee) employee;
               hourlyEmp.HoursWorked = e.Hours;
            }
         }
      }
   }
}


// Enumeration indicating type of messages incoming
public enum MsgType { Add, Payroll }

/// <summary>
/// Base class for incoming messages, contains type of message.
/// </summary>
abstract class EmployeeMessage
{
   public MsgType m_Type;
   
   public EmployeeMessage( MsgType Type )
   {
      m_Type = Type;
   }
   
   public MsgType MessageType { get { return m_Type; } }
}

/// <summary>
/// Defines the data that will be passed from the delegate to the
/// the callback method when the event is raised, in this case
/// the employee information.
/// </summary>
class AddEmployeeMessage : EmployeeMessage
{
   int m_ID;
   string m_FirstName; 
   string m_LastName;
   string m_MiddleName;
   string m_SSN;
   bool m_IsSalaried;
   double m_PayRate;

   public AddEmployeeMessage( MsgType type, int ID, 
      string FirstName, string LastName, string MiddleName, 
      string SSN, bool IsSalaried, double PayRate ) :
      base( type )
   {
      m_ID = ID;
      m_FirstName = FirstName;
      m_LastName = LastName;
      m_MiddleName = MiddleName;
      m_SSN = SSN;
      m_IsSalaried = IsSalaried;
      m_PayRate = PayRate;
   } 
   
   // Event argument properties contain the data to pass to the
   // callback methods subscribed to the event.
   public int ID            { get { return m_ID; } }
   public string FirstName  { get { return m_FirstName; } }
   public string LastName   { get { return m_LastName; } }
   public string MiddleName { get { return m_MiddleName; } }
   public string SSN        { get { return m_SSN; } }
   public bool IsSalaried   { get { return m_IsSalaried; } }
   public double PayRate    { get { return m_PayRate; } }
}


/// <summary>
/// Defines the data that will be passed from the delegate to the
/// the callback method when the event is raised, in this case
/// the hours worked information.
/// </summary>
class PayrollMessage : EmployeeMessage
{
   int m_ID;
   double m_Hours; 

   public PayrollMessage( MsgType type, int ID, 
      double Hours ) :
      base( type )
   {
      m_ID = ID;
      m_Hours = Hours;
   } 
   
   // Event argument properties contain the data to pass to the
   // callback methods subscribed to the event.
   public int ID       { get { return m_ID; } }
   public double Hours { get { return m_Hours; } }
}

/// <summary>
/// Class that simulates a message queue. The queue contains add 
/// employee messages, type "a" and payroll information messages,
/// type "p".                 
/// </summary>
class EmployeeMessageQueue
{
   private int m_lengthQueue;
   private int m_nCurPos = 0;

   private string[, ] m_msgQueue =
   {
      {"a", "1", "Timothy", "Arthur", "Tucker", "555-55-5555", "salaried", "60000" },
      {"a", "2", "Sally", "Bess", "Jones", "666-66-6666", "hourly", "25.86" },
      {"a", "3", "Jeff", "Michael", "Simms", "777-77-7777", "hourly", "15.12" },
      {"a", "4", "Janice", "Anne", "Best", "888-88-8888", "salaried", "24000" },
      {"a", "5", "Fred", "Alan", "Danson", "999-99-99990", "salaried", "24000" },
      {"p", "2", "27", "", "", "", "", "" },
      {"p", "3", "32", "", "", "", "", "" }
   };

   public EmployeeMessageQueue()
   {
      m_lengthQueue = 7;
   }
   
   public bool moreMessages()
   {
      if ( m_nCurPos < m_lengthQueue )
         return true;
      else
         return false;
   }
   
   /// <summary>
   /// Gets the next message in the queue if any.
   /// </summary>
   public EmployeeMessage getNextMessage()
   {
      EmployeeMessage msg = null;
 
      try
      {
         if ( moreMessages() )
         {
            string Action = m_msgQueue[m_nCurPos,0];
         
            switch ( Action )
            {
               case "a":
                  msg = processAddEmployeeRequest( m_nCurPos );
                  break;
               
               case "p":
                  msg = processAddHoursRequest( m_nCurPos );
                  break;
            }
         }
         else
         {
            throw new EndOfMessageQueueException( 
               "Attempt to read past end of queue!" );
         }
      }
      catch ( Exception e )
      {
         throw e;
      }
      finally
      {
         m_nCurPos++;
      }
   
      return msg;
   }
   
   /// <summary>
   /// Constructs a new add employee message instance
   /// </summary>
   private EmployeeMessage processAddEmployeeRequest( int index )
   {
      int ID = Convert.ToInt32( m_msgQueue[index,1] );
      string FirstName = m_msgQueue[index,2];
      string MiddleName = m_msgQueue[index,3];
      string LastName = m_msgQueue[index,4];
      string SSN = m_msgQueue[index,5];
      
      bool IsSalaried = false;
      if ( m_msgQueue[index,6] == "salaried" )
         IsSalaried = true;
         
      double PayRate = Convert.ToDouble( m_msgQueue[index,7] );

      if ( SSN.Length != 11 )
         throw new ArgumentOutOfRangeException( 
            "SSN is of incorrect length!" );

      Console.WriteLine( "Invoking delegate" );
      AddEmployeeMessage msg = new AddEmployeeMessage( MsgType.Add, 
         ID, FirstName, LastName, MiddleName, SSN, IsSalaried, 
         PayRate );

      return msg;
   }

   /// <summary>
   /// Constructs a new payroll hours message instance
   /// </summary>
   private EmployeeMessage processAddHoursRequest( int index )
   {
      int ID = Convert.ToInt32( m_msgQueue[index,1] );
      double Hours = Convert.ToDouble( m_msgQueue[index,2] );

      PayrollMessage msg = new PayrollMessage( MsgType.Payroll, 
         ID, Hours );
         
      return msg;
   }
}

/// <summary>
/// Custom exception which is thrown when an attempt is made to  
/// read past the end of the queue.
/// </summary>
class EndOfMessageQueueException : Exception
{
   public EndOfMessageQueueException( string Message ) :
      base( Message )
   {
   }
}

/// <summary>
/// General logging class to a file. Base class for other more
/// specific loggers.
/// </summary>
class Logger
{
   string m_fileName;

   public Logger( string fileName )
   {
      m_fileName = fileName;
   }

   protected void log( string text )
   {
      FileStream stream = new FileStream( m_fileName, 
         FileMode.OpenOrCreate, FileAccess.ReadWrite);

      StreamWriter writer = new StreamWriter( stream );
      writer.BaseStream.Seek( 0, SeekOrigin.End );

      writer.Write("{0} {1} \n", DateTime.Now.ToLongTimeString(),
         DateTime.Now.ToLongDateString());

      writer.Write( text ); 
      writer.Write("\n------------------------------------\n\n");
      writer.Flush();
      writer.Close();
   }
}

/// <summary>
/// Writes add employee events to a log file.
/// </summary>
class EmployeeQueueLogger : Logger
{
   public EmployeeQueueLogger( string filename ) :
      base( filename )
   {
   }

   public void logAddRequest( object sender, 
      AddEmployeEventArgs e )
   {
      string name = e.FirstName + " " + e.MiddleName + " " + 
         e.LastName;
         
      string text = "Adding Employee\n";
      text += "EmployeeID: " + e.ID.ToString();
      text += ", Name: " + name;
      log( text );
   }

   public void logHoursWorked( object sender, 
      HoursWorkedEventArgs e )
   {
      string text = "Adding Hours Worked\n";
      text += "EmployeeID: " + e.ID.ToString();
      text += ", Hours Worked: " + e.Hours.ToString();
      log( text );
   }
}

/// <summary>
/// Logs error meessage to a log file.
/// </summary>
class ErrorLogger : Logger
{
   public ErrorLogger( string filename ) :
      base( filename )
   {
   }

   public void logError( Exception exception )
   {
      log( exception.Message );
      log( exception.StackTrace );
   }
}


/// <summary>
/// Container class for employees derived from ArrayList 
/// </summary>
class Employees : ArrayList
{
   public int Length
   {
      get { return this.Count; }
   }
}

/// <summary>
/// Base class for an employee. Note that this is an abstract class
/// and therefore cannot be instantiated.
/// </summary>
abstract class Employee
{
   private int m_ID;
   private string m_firstName;
   private string m_middleName;
   private string m_lastName;
   private string m_SSN;

   public Employee( int ID, string FirstName, string LastName, 
      string MiddleName, string SSN )
   {
      m_ID = ID;
      m_firstName = FirstName;
      m_middleName = MiddleName;
      m_lastName = LastName;
      m_SSN = SSN;
   }

   abstract public double Payroll
   {
      get;
   }

   public int ID
   {
      get { return m_ID; }
   }

   public string FirstName
   {
      get { return m_firstName; }
      set { m_firstName = value; }
   }

   public string MiddleName
   {
      get { return m_middleName; }
      set { m_middleName = value; }
   }

   public string LastName
   {
      get { return m_lastName; }
      set { m_lastName = value; }
   }

   public string SSN
   {
      get { return m_SSN; }
      set { m_SSN = value; }
   }
}

/// <summary>
/// Salaried employee class. Implements the abstract method Payroll
/// defined in the base class.
/// </summary>
class SalariedEmployee : Employee
{
   private double m_Salary;

   public SalariedEmployee( int ID, string FirstName, 
      string LastName, string MiddleName,string SSN, 
      double Salary ) :
      base( ID, FirstName, LastName, MiddleName, SSN )
   {
      m_Salary = Salary;
   }

   override public double Payroll
   {
      get { return m_Salary / 12; }
   }
}

/// <summary>
/// Hourly employee class. Implements the abstract method Payroll
/// defined in the base class. Also implements some class
/// specific methods
/// </summary>
class HourlyEmployee : Employee
{
   private double m_HourlyRate;
   private double m_HoursWorked;

   public HourlyEmployee( int ID, string FirstName, 
      string LastName, string MiddleName, string SSN, 
      double HourlyRate ):
      base( ID, FirstName, LastName, MiddleName, SSN )
   {
      m_HourlyRate = HourlyRate;
      m_HoursWorked = 0;
   }

   public double HoursWorked
   {
      get { return m_HoursWorked; }
      set { m_HoursWorked = value; }
   }

   override public double Payroll
   {
      get { return m_HoursWorked * m_HourlyRate; }
   }
}

