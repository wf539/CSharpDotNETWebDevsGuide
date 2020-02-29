using System;
using System.Collections;
using System.IO;

/// <summary>
/// Contains the entry point for the Events sample
/// </summary>
class EventsSample
{
   static void Main( string[] args )
   {
      try
      {
         // Create a container to hold employees
         Employees employees = new Employees(4);

         // Create and drain our simulated message queue
         EmployeeQueueMonitor monitor = 
            new EmployeeQueueMonitor( employees );
        
         monitor.start();
         monitor.stop();   
         
         // Display the employee list on screen
         Console.WriteLine( "List of employees added via delegate:" );
         foreach ( Employee employee in employees )
         {
            string name = employee.FirstName + " " + 
               employee.MiddleName + " " + employee.LastName;

            string ssn = employee.SSN;
                        
            Console.WriteLine( "Name: {0}, SSN: {1}", name, ssn );
         }
      }
      catch ( Exception exception )
      {
         // Display any errors on screen
         Console.WriteLine( exception.Message );
      }
   }
}


/// <summary>
/// Defines the data that will be passed from the event delegate to
/// the the callback method when the event is raised
/// </summary>
class AddEmployeEventArgs : EventArgs
{
   string m_FirstName; 
   string m_LastName;
   string m_MiddleName;
   string m_SSN;

   public AddEmployeEventArgs( string FirstName, 
      string LastName, string MiddleName, string SSN )
   {
      m_FirstName = FirstName;
      m_LastName = LastName;
      m_MiddleName = MiddleName;
      m_SSN = SSN;
   } 
   
   // Event argument properties contain the data to pass to the
   // callback methods subscribed to the event.
   public string FirstName { get { return m_FirstName; } }
   public string LastName { get { return m_LastName; } }
   public string MiddleName {get { return m_MiddleName; } }
   public string SSN { get { return m_SSN; } }
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
      
   // Instance of the AddEmployeeEvent 
   public event AddEmployeeEvent OnAddEmployee;
   
   private EmployeeQueueLogger m_logger;
   
   private Employees m_employees;
   private int m_lengthQueue;

   private string[, ] m_msgQueue =
   {
      {"Timothy", "Arthur", "Tucker", "555-55-5555"},
      {"Sally", "Bess", "Jones", "666-66-6666" },
      {"Jeff", "Michael", "Simms", "777-77-7777"},
      {"Janice", "Anne", "Best", "888-88-8888" }
   };

   public EmployeeQueueMonitor( Employees employees )
   {
      m_employees = employees;
      m_lengthQueue = 4;

      m_logger = new EmployeeQueueLogger( "log.txt" );

      // Register the methods that the Event will invoke when an add 
      // employee message is read from the message queue
      OnAddEmployee += 
         new AddEmployeeEvent( this.addEmployee );
      OnAddEmployee += 
         new AddEmployeeEvent( m_logger.logAddRequest );
   }

   // Drain the queue.
   public void start()
   {
      if ( m_employees == null )
         return;
      
      for ( int i = 0; i < m_lengthQueue; i++ )
      {
         // Pop an add employee request off the queue
         string FirstName = m_msgQueue[i,0];
         string MiddleName = m_msgQueue[i,1];
         string LastName = m_msgQueue[i,2];
         string SSN = m_msgQueue[i,3];

         Console.WriteLine( "Invoking delegate" );
         
         // Create the event arguments to pass to the methods
         // subscribed to the event and then invoke event resulting
         // in the callbacks methods being executed, namely 
         // Employees.this.addEmployee() and 
         // EmployeeQueueLogger.logAddRequest()         
         AddEmployeEventArgs args = new AddEmployeEventArgs( FirstName,
            LastName, MiddleName, SSN );
         OnAddEmployee( this, args );
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

      int index = m_employees.Length;
      m_employees[index] = new Employee ( e.FirstName, e.MiddleName, 
         e.LastName, e.SSN );
   }
}


/// <summary>
/// Writes add employee events to a log file.
/// </summary>
class EmployeeQueueLogger
{
   string m_fileName;

   public EmployeeQueueLogger( string fileName )
   {
      m_fileName = fileName;
   }

   // Called by event whenever a new add employee message appears
   // in the message queue. Notice the signature matches that requried
   // by System.Event   
   public void logAddRequest( object sender, AddEmployeEventArgs e )
   {
      string name = e.FirstName + " " + e.MiddleName + " " + 
         e.LastName;

      FileStream stream = new FileStream( m_fileName, 
         FileMode.OpenOrCreate, FileAccess.ReadWrite);

      StreamWriter writer = new StreamWriter( stream );
      writer.BaseStream.Seek( 0, SeekOrigin.End );

      writer.Write("{0} {1} \n", DateTime.Now.ToLongTimeString(),
         DateTime.Now.ToLongDateString());

      writer.Write( "Adding employee - Name: {0}, SSN: {1}", 
         name, e.SSN );
      writer.Write("\n------------------------------------\n\n");
      writer.Flush();
      writer.Close();
   }
}


/// <summary>
/// Container class for employees. This class implements 
/// IEnumerable allowing use of foreach sytax
/// </summary>
class Employees : IEnumerable
{
   private ArrayList m_Employees;
   private int m_MaxEmployees;

   public Employees( int MaxEmployees )
   {
      m_MaxEmployees = MaxEmployees;
      m_Employees = new ArrayList( MaxEmployees );
   }   

   // Here is the implementation of the indexer by array index
   public Employee this[int index]
   {
      get 
      {
         // Check for out of bounds condition
         if ( index < 0 || index > m_Employees.Count - 1 )
            return null;
         
         // Return employee based on index passed in      
         return (Employee) m_Employees[index];    
      }

      set 
      {
         // Check for out of bounds condition
         if ( index < 0 || index > m_MaxEmployees-1 )
            return;

         // Add new employee
         m_Employees.Insert( index, value );
      }
   }

   // Here is the implementation of the indexer by SSN
   public Employee this[string SSN]
   {
      get 
      {
         Employee empReturned = null;

         foreach ( Employee employee in m_Employees )
         {
            // Return employee based on index passed in      
            if ( employee.SSN == SSN )
            {
               empReturned = employee;
               break;
            }
         }
      
         return empReturned;
      }
   }

   // Return the total number of employees.
   public int Length 
   {
      get 
      {
         return m_Employees.Count;
      }
   }

   // IEnumerable implementation, delegates IEnumerator to
   // the ArrayList
   public IEnumerator GetEnumerator()
   {
      return m_Employees.GetEnumerator();
   }
}


/// <summary>
/// Represents a single employee
/// </summary>
class Employee
{
   private string m_firstName;
   private string m_middleName;
   private string m_lastName;
   private string m_SSN;

   // Constructor
   public Employee( string FirstName, string LastName, string  
      MiddleName, string SSN )
   {
      m_firstName = FirstName;
      m_middleName = MiddleName;
      m_lastName = LastName;
      m_SSN = SSN;
   }
   
   // FirstName property
   public string FirstName
   {
      get { return m_firstName; }
      set { m_firstName = value; }
   }

   // MiddleName property
   public string MiddleName
   {
      get { return m_middleName; }
      set { m_middleName = value; }
   }

   // LastName property
   public string LastName
   {
      get { return m_lastName; }
      set { m_lastName = value; }
   }

   // SSN property
   public string SSN
   {
      get { return m_SSN; }
      set { m_SSN = value; }
   }
}
