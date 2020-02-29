using System;
using System.Collections;

/// <summary>
/// Contains the program entry point for the Indexers Sample.
/// </summary>
class IndexersSample
{
   static void Main( string[] args )
   {
      try
      {
         // Create a container to hold employees
         Employees employees = new Employees(4);
   
         // Add some employees
         employees[0] = new Employee ( "Timothy", "Arthur", 
            "Tucker", "555-55-5555" );
         
         employees[1] = new Employee ( "Sally", "Bess", 
            "Jones", "666-66-6666" );
         
         employees[2] = new Employee ( "Jeff", "Michael", 
            "Simms", "777-77-7777" );
            
         employees[3] = new Employee ( "Janice", "Anne", 
            "Best", "888-88-8888" );
         
         // Display the employee list on screen
         for ( int i = 0; i < employees.Length; i++ )
         {
            string name = employees[i].FirstName + " " + 
               employees[i].MiddleName + " " + 
               employees[i].LastName;

            string ssn = employees[i].SSN;
                        
            Console.WriteLine( "Name: {0}, SSN: {1}", name, ssn );
         }

         Employee employee = employees["777-77-7777"];
         if ( employee != null )
         {
            string name = employee.FirstName + " " + 
               employee.MiddleName + " " + employee.LastName;

            string ssn = employee.SSN;
                        
            Console.WriteLine( "Found by SSN, Name: {0}, SSN: {1}", 
               name, ssn );
         }
         else
         {
            Console.WriteLine( 
               "Could not find employee with SSN: 777-77-7777" );
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
/// Container class for employees. This class implements two 
/// indexers
/// </summary>
class Employees
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
