using System;

/// <summary>
/// Contains the program entry point for the Properties Sample.
/// </summary>
class PropertySample
{
   static void Main( string[] args )
   {
      try
      {
         // Create a new employee
         Employee employee = new Employee();
         
         // Set some properties
         employee.FirstName = "Timothy";
         employee.MiddleName = "Arthur";
         employee.LastName = "Tucker";
         employee.SSN = "555-55-5555";
         
         // Show the results on screen
         string name = employee.FirstName + " " + employee.MiddleName +
            " " + employee.LastName;
         string ssn = employee.SSN;
            
         Console.WriteLine( "Name: {0}, SSN: {1}", name, ssn );
      }
      catch ( Exception exception )
      {
         // Display any errors on screen
         Console.WriteLine( exception.Message );
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
