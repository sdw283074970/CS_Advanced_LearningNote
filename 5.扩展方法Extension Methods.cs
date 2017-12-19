//Q: 什么是扩展方法?扩展方法能干什么？
//A: 扩展方法(Extension Methods)是一种写法，用来在不改变已存在类的源代码、不继承类的情况下给这些已存在类添加新的方法。

//Q: 为什么我们要使用扩展方法？
//A: 很多情况下我们没有目标类的源代码。使用扩展方法最大的好处就是不用修改类的源代码就能添加新方法。

//Q: 继承也不用修改源代码，那为什么不用继承？
//A: 有些类的封装修饰符为密封(seal)，我们无法继承。在这种情况下若执意添加新方法，扩展方法是唯一的选择。

//Q: 如何创建扩展方法？命名上有没有什么格式？
//A: 创建的扩展方法收集在一个类中，该必须为非泛型静态类(static)，命名遵循“添加新方法的目标类名”+“Extensions”的格式。如给String类加入新方法：

public static class StringExtensions 
{
  //...具体方法，方法也必须为静态(static)，因为静态类成员必须为静态成员
}

//举个例子，string中没有“返回前n位字符”的方法，我们不知道string的源代码，其又是seal的，我们可以使用扩展方法为string添加这个新方法：


public static class StringExtensions 
{
  public static string Shorten(this string str, int numberOfWords)  //扩展方法和普通方法的区别就在“this string str”上，通过对象+.调用
  {
    if (numberOfWords < 0)
      throw new ArgumentOutOfRangeException("number of words cannot be negative.")
      
    if (numberOfWords == 0)
      return "";
      
    var words =str.Split(' ');
    
    if (words.Length <= numberOfWords)
      return words;
      
    return string.Join(" ", words.Take(numberOfWords)) + "...";
  }//简单方法不作赘述
}

//这样我们就能通过string来调用Shorten方法，如在Main()中：

static void Main(string[] args)
{
  string post = "You are a madafaka!";
  var shortenstring = post.Shorten(11);
}//输出结果为"You are a m..."

//Q: 为什么扩展方法必须为静态(static)类？
//A: 使用扩展方法的目的是像访问旧方法一样访问新方法，即可通过“对象.新方法”访问。非静态类必须实例化才能访问其成员方法，那么就跟最初的目的背道而驰，
  //而静态类成员必须也是静态，所以无论是扩展方法还是该方法存在的类，都必须是静态类，使用关键词this构造方法。
  
//Q: 是否可以随心所欲使用扩展方法？
//A: 微软的官方指南不建议自创扩展方法，除非迫不得已的情况。只要会使用，并知道用的是扩张方法就够了，如LINQ的方法大部分都是扩展方法。原因之一是，
  //若目标类的原作者/其他人在类中加入了与自己写的重名方法但细节不一样，那么自己写的静态方法不会被优先执行又不容易被察觉，以至于扰乱正常生产。

//暂时想到这么多，最后更新2017/11/14
