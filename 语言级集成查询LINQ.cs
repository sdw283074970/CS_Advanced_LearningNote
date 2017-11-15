//Q: 什么是LINQ？这是干什么的？
//A: LINQ指的是System.Colection.Generic.Linq下的所有类、方法。LINQ代表语言级集成查询(Language Integrated Query)，通过调用其中的类、方法让我们
  //具备查询的能力。
  
//Q: 我能用LINQ查询什么？
//A: 按照对象载体类别可以分为四类，分别是：
  //1. 查询内存中的对象，如集合(Collection)；
  //2. 查询实体对象，如SQLite数据库；
  //3. 查询XML对象；
  //4. 查询数据集，如ADO.NET Sata Sets.
//这里主要讨论第一种情况。

//Q: 为什么我们要用LINQ查询？
//A: LINQ能为我们提供一套类似于SQL的查询语句，读写简单清楚，易于操作和维护。在没有LINQ的情况下，我们要查找满足条件所写的代码冗长又复杂，如
  //查询书单列表中所有价格低于10的书的代码如下：
  
static void Main(string[] args)
{
  Var books = new BookRepository().GetBooks();
  
  var cheapBooks = new List<Book>();
  foreach (var b in books)
  {
    if (b.Price < 10)
      cheapBooks.Add(b);
  }
  foreach (var b in cheapBooks)
    Console.WriteLine(b.Title + " " + book.Price);
}

