using System;
using System.Globalization;
using System.IO;

class MyException : Exception
{
}

public class Help
{
	public static Exception s_exceptionToThrow;
	public static bool s_matchingException;

	public static Object s_object = new object();
}
public class A<T>
where T: Exception
{
    public static void StaticFunctionWithFewArgs()
    {
        try
        {
            throw Help.s_exceptionToThrow;
        }
        catch (T match)
        {
            if (!Help.s_matchingException)
                throw new Exception("This should not have been caught here", match);

            Console.WriteLine("Caught matching " + match.GetType());
        }
        catch (Exception mismatch)
        {
            if (Help.s_matchingException)
                throw new Exception("Should have been caught above", mismatch);

            Console.WriteLine("Expected mismatch " + mismatch.GetType());
        }
    }
}


public class GenericExceptions
{

	public static void StaticFunctionWithFewArgs()
	{
		Help.s_matchingException = true;
		Help.s_exceptionToThrow = new MyException();
		A<Exception>.StaticFunctionWithFewArgs();
		A<MyException>.StaticFunctionWithFewArgs();

		Help.s_matchingException = false;
		Help.s_exceptionToThrow = new Exception();
		A<MyException>.StaticFunctionWithFewArgs();
	}


        [System.Runtime.CompilerServices.MethodImpl(
          System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
	public static int Main()
	{
	    try
	    {
		Console.WriteLine("This test checks that we can catch generic exceptions.");
		Console.WriteLine("All exceptions should be handled by the test itself");

		StaticFunctionWithFewArgs();
		
	    }
	    catch(Exception)
	    {
		Console.WriteLine("Test Failed");
		return -1;
	    }

	    Console.WriteLine("Test Passed");
	    return 100;
	}

}
