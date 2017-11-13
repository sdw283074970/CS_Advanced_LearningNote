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
  
//args指传入参数，=>读作goes to，expression指核心表达式。如以上例子用朗母达表达式可写成“n => n * 5”，其返回值类型系统会自动推断
  //那么如何在实际中使用？这个时候我们需要引入委托来传递给匿名函数用的参数，以上程序可改写为:

static void Main(string[] args)
{
  Func<int, int> mult = Mult;  //实例化委托及指向的方法Sqaure，Func中的第一参数类型为传入参数类型，对应int n，第二个参数类型为返回类型
  //mult += ... 这里可以干些其他事
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
  //mult += ... 这里可以干些其他事
  Console.WriteLine(mult(10));
}

//Q: 如果传入参数为空匿名方法应该怎么写？
//A: 写作 () => expression 就行了，另外如果有多个传入参数，则写作 (x, y, z) => expression

//Q: 匿名表达式只能被用于简写委托的指向方法？
//A: 是的，因为匿名表达式需要委托来传递参数和参数类型，而委托也需要将一个方法添加进委托列表。举一个查找价格低于10的书的例子，并打印出书的名字:

static void Main(string[] args)
{
  //实例化一个类，过程为先实例BookRepository，再调用其中的GetBook()函数，返回的是一个List<Book>，再赋值给books，即books为一个List<Book>类
  var books = new BookRepository().GetBooks();
  //调用List<Book>类中的FindAll方法，匿名表达式指传入book类实例，并判断其价格是否大于10，返回一个Boolean值给FindAll方法
  var cheapBooks = books.FindAll(b => b.Price < 10); 
  foreach (var book in cheapBooks)
  {
    Console.WriteLine(book.Title);  //将结果迭代打印出来
  }
}

//以上例子中，Book类和BookRepository类省略掉了，Book类就一个书名属性一个价格属性，BookRepository就一个GetBook()方法，包含了一个实例化的Book类
  //列表。这里需要说明一下List<T>类中的一系列Find方法，这些方法都需要传入Predicate<T> match类，在Collection中特别常见。
//List<T>.FindAll()方法是一个迭代方法，迭代列表所有成员，将每个成员作为参数传递给Predicate<T>委托，在这里T为Book，返回类型为List<Book>；
  //Predicate是一种泛型委托，指向的是一个返回值类型Boolean的方法，这个方法由我们来写，在这里我们可以专门写一个返回类型为布尔值的方法，也可以直接
  //用匿名方法，换句话说，List<T>.FindAll()括号中必须为一个方法，不能是单纯的布尔值。

//暂时想到这么多，最后更新2017年11月10日
