//Q: 什么是异常处理？
//A: 异常处理(Exception Handling)是一种机制，用来处理程序中出现的异常情况，即超出程序正常执行的一些特殊条件。

//Q: 为什么要异常处理？
//A: 一个原因是异常情况会直接导致程序崩溃，然后我们只知道潜在出错的地方但不知道具体错误出在哪里；第二个原因是有时候系统抛出异常后我们并不想让程序终止，
  //而想让程序执行一个替代方法，这个时候我们就需要异常处理机制。举个例子，在除法中分母不能为零，倘若我们给分母赋值为0，则程序会崩溃，并抛出异常。
  //代码如下：

public class Div  
{
  public int Divide(int n1, int n2)  //定义一个基本的除法
  {
    return n1 / n2;
  }
}

static void Main(string[] args)
{
  Console.WriteLine(Divide(5, 0));  //打印出5除以0的结果
}

//这个时候控制台会抛出异常，并终止程序。在控制台中我们可以看到未能处理的异常：

Unhandle Exception: System.DivideByZeroException: Attempted to divide by zero.

//我们可以看出抛出的异常也是一个类，为在 System 中的 DivideByZeroException 类。我们还会看到控制台按顺序指出哪个参数、哪个方法、哪个类、哪个命名空间、
  //哪个文件中的哪一行出了问题。这个过程叫做栈追踪(Stack Trace)，通过反向追踪函数调用轨迹来查找出问题的部分。如果没有异常处理，即出现 Unhandle 
  //Exception 的情况，系统会调用终止函数来让整个程序强制结束，即俗称程序崩溃。

//Q: 如何处理异常？
//A: 使用 try&catch 代码块，即在try的代码块中抛出异常，那么直接执行catch代码块的命令。在catch中，我们可以选择直接抛出异常，给出一些更有实际意义的
  //报错语句，或把异常作为内部异常，使用一个新的自定义异常类重新抛出异常。继续以以上为例直接抛出异常，不过换个用户友好的表达：

static void Main(string[] args)
{
  try
  {
    Console.WriteLine(Divide(5, 0));  //打印出5除以0的结果
  }
  catch(Exception)
  {
    Console.WriteLine("Sorry, we cannot divide by 0.");
  }
}

//以上运行结果仅为一条"Sorry, we cannot divide by 0."，这样反馈更有可读性，没有复杂的 Stack Trace 结果，并且程序不会崩溃。

//Q: catch 中的 Exception 是什么？
//A: 正如前面所说，异常 Exception 也是所有异常类的基类，catch 块需要访问一个异常类，这个类的成员记录了所有相关的异常信息，如Message记录了异常描述，
  //Source记录了产生异常的应用名（程序集或dll），StackTrace记录了调用方法的反向轨迹，TargetSite记录了抛出异常的方法等等。
  //我们可以很容易调用这些信息，如：

  catch(Exception e)
  {
    Console.WriteLine(e.Message);
    Console.WriteLine(e.Source);
    Console.WriteLine(e.StackTrace);
  }

//除了以上四个常用的属性，此外还有InnerException记录了被插入到这个异常中的其他异常。
//在某些情况下，一个代码块可能会抛出多种不同的异常，这个时候我们就需要多重catch块来对异常进行匹配，匹配到的第一个catch代码块将被执行，其他catch块
  //将不被执行，并且catch块的顺序必须由具体到泛型，如：

static void Main(string[] args)
{
  try
  {
    Console.WriteLine(Divide(5, 0));  //打印出5除以0的结果
  }
  catch(DivideByZeroException e)  //最具体的类，其父类为 ArithmeticException
  {
    Console.WriteLine("Sorry, we cannot divide by 0.");
  }
  catch(ArithmeticException e)  //其父类为SystemException
  {
    Console.WriteLine("Sorry, we cannot divide by 0.");
  }
  catch(SystemException e)  //其父类为Exception
  {
    Console.WriteLine("Sorry, we cannot divide by 0.");
  }
    catch(Exception e)  //所有异常类的基类
  {
    Console.WriteLine("Sorry, we cannot divide by 0.");
  }
}


















