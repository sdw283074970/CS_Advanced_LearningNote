//Q: 什么是匿名方法？
//A: 匿名方法，又称朗母达表达式，是一个没有封装标记(如Public)，没有名字，没有返回语句表述的三无方法。

//Q: 为什么我们要使用匿名方法？
//A: 因为懒，不想打太多字，能省一点是一点，而且读起来更方便。

//Q: 使用匿名方法有什么惩罚吗？
//A: 完全没有。我们可以用匿名方法达到与正常表达完全相同的结果。

//Q: 既然这么好，为什么不全使用匿名方法？
//A: 因为使用匿名方法有限制，只有简单的内容一两句就能讲清楚的方法可以用匿名方法替代，其他复杂的不行。以下举一乘法的例子:

static void Main(string[] args)
{
  Console.WriteLine(Mult(10));
}

static int Mult(int n)
{
  return n * 5;
}

//以上输出结果为50，很简单。现在我们可以用匿名方法来达到同样的效果，代码更简介。朗母达表达式的格式为
  
  //args => expression
  
//args指传入参数，=>读作goes to，expression指核心表达式。如以上例子用朗母达表达式可写成“n => n * 5”
//那么如何在实际中使用？这个时候我们需要继续引入委托的概念。在有委托的情况下以上程序可改写为:

static void Main(string[] args)
{
  Func<int, int> mult = Mult;  //实例化委托及指向的方法Sqaure，Func中的第一参数类型为传入参数类型，对应int n，第二个参数类型为返回类型
  //square += ... 这里可以干些其他事
  Console.WriteLine(mult(10));
}

static int Mult(int n)
{
  return n * 5;
}

//我们可以通过朗母达表达式省去Mult方法并替换它在委托中的位置，如

static void Main(string[] args)
{
  const int facot = 5;  //定义一个常量
  Func<int, int> mult = n => n * factor;  //使用匿名方法替换之前的方法，并且匿名方法有权限引用方法内的所有变量，如factor
  //square += ... 这里可以干些其他事
  Console.WriteLine(mult(10));
}
