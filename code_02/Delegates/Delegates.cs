using System;
using System.Collections;

/// <summary>
/// Contains the program entry point for the Delegates Sample.
/// </summary>
class DelegatesSample
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
         Console.WriteLine( 
            "List of employees added via delegate:" );
            
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
/// Simulates our message queue.
/// </summary>
class EmployeeQueueMonitor
{
   // Delegate signature
   public delegate void AddEventCallback( string FirstName, 
      string LastName, string MiddleName, string SSN );
      
   // Instance of the delegate
   private AddEventCallback m_addEventCallback;
   
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

      // Create an instace of the delegate and register the 
      // addEmployee method of this class as a callback.
      m_addEventCallback = new AddEventCallback( 
         this.addEmployee );
   }

   // Drain the queue.
   public void start()
   {
      if ( m_employees == null )
         return;
      
      for ( int i = 0; i < m_lengthQueue; i++ )
      {
         string FirstName = m_msgQueue[i,0];
         string MiddleName = m_msgQueue[i,1];
         string LastName = m_msgQueue[i,2];
         string SSN = m_msgQueue[i,3];

         // Invoke the callback registered with the delegate
         Console.WriteLine( "Invoking delegate" );
         m_addEventCallback( FirstName, LastName, MiddleName, 
            SSN );
      }
   
   }
   
   public void stop()
   {
      // In a real communications program you would shut down
      // gracefully.
   }
   
   // Called by the delegate when a an message to add an employee
   // is read from the message queue.
   public void addEmployee( string FirstName, string MiddleName,
      string LastName, string SSN )
   {
      Console.WriteLine( "In delegate, adding employee\r\n" );

      int index = m_employees.Length;
      m_employees[index] = new Employee ( FirstName, MiddleName, 
         LastName, SSN );
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
