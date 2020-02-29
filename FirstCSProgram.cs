using System;

namespace FirstCSharpProgram

{

  ///<summary>
  ///My first C# class. Contains the program entry point.
  ///</summary>

  class FirstCSharpClass
  {

    [STAThread]
    static void Main(string[] args)
    {

      try
      {
        /*
         * Show when we wrote our first program on screen
         */
         DateTime today = DateTime.Now;
         Console.WriteLine("I executed (as opposed to wrote) my first C# program at: " + today.ToString());
         if (args.Length > 0)
         {

           // Show an optional message on screen.
           string msg = "You wanted to say: " + args[0];
           Console.WriteLine(msg);

         }
      }

      catch (Exception exception)
      {
          // Display any errors on screen
          Console.WriteLine(exception.Message);
      }
    }
  }
}
