//Q: 什么是异常处理？
//A: 异常处理(Exception Handling)是一种机制，用来处理程序中出现的异常情况，即超出程序正常执行的一些特殊条件。

//Q: 为什么要异常处理？
//A: 一个原因是异常情况会直接导致程序崩溃，然后我们只知道潜在会出错的地方但不知道具体错误出在哪里，一个一个点排查太不效率；第二个原因是有时候系统抛出异常
  //后我们并不想让程序终止，而想让程序执行一个替代方法，这个时候我们就需要异常处理机制。
  //举个例子，在除法中分母不能为零，倘若我们给分母赋值为0，则程序会崩溃，并抛出异常。代码如下：


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
