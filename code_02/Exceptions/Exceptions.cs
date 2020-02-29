using System;
using System.Collections;

/// <summary>
/// Contains the program entry point for the Exceptions Sample.
/// </summary>
class ExceptionsSample
{
   static void Main( string[] args )
   {
      try
      {
         // Create a container to hold employees
         Employees employees = new Employees(4);
   
         // Add some employees
         addOneEmployee ( employees, "Timothy", "Arthur", 
            "Tucker", "555-55-5555" );
         
         addOneEmployee ( employees, "Sally", "Bess", 
            "Jones", null );
         
         addOneEmployee ( employees, "Jeff", "Michael", 
            "Simms", "777-77-7777" );
            
         addOneEmployee ( employees, "Janice", "Anne", 
            "Best", "9888-88-88889" );
         
         // Display the employee list on screen
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
   
   // Helper method to add an employee to the list
   static void addOneEmployee( Employees employees,  
      string FirstName, string MiddleName, string LastName, 
      string SSN )
   {
      bool addedEmployee = false;

      try
      {
         Console.WriteLine( "Adding an employee" );

         // SSN cannot be NULL, throw exception
         if ( SSN == null )
            throw new ArgumentNullException( "SSN is null!" );

         // SSN length must be 11, throw exception
         if ( SSN.Length != 11 )
            throw new ArgumentOutOfRangeException( 
               "SSN length invalid!" );

         // Add the employee
         employees[employees.Length] = new Employee ( FirstName, 
            MiddleName, LastName, SSN );

         addedEmployee = true;
      }
      catch ( ArgumentOutOfRangeException exception )
      {
         Console.WriteLine( "We caught ArgumentOutOfRangeException"  );
         Console.WriteLine( exception.Message );
      }
      catch ( ArgumentNullException exception )
      {
         Console.WriteLine( "We caught ArgumentNullException"  );
         Console.WriteLine( exception.Message );
      }
      catch ( Exception exception )
      {
         Console.WriteLine( "We caught a base exception"  );
         Console.WriteLine( exception.Message );
      }
      catch 
      {
         Console.WriteLine( "We caught an unknown exception"  );
         Console.WriteLine( "Unknown exception caught!" );
      }
      finally
      {
         if ( addedEmployee == true )
            Console.WriteLine( "Add was successful\r\n" );
         else
            Console.WriteLine( "Add failed\r\n" );
      }
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
