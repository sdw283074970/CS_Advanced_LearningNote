//Q: 什么是LINQ？这是干什么的？
//A: LINQ指的是System.Linq下的所有类、方法。LINQ代表语言级集成查询(Language Integrated Query)，通过调用其中的类、方法让我们具备查询的能力。
  
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

//在SQL中以上查询只用一句就能表达，即SELECT * FROM books WHERE price<10;
//有了LINQ我们也可以用类似的关键词Where实现一句话完成陈述，如下：

static void Main(string[] args)
{
  Var books = new BookRepository().GetBooks();
  var cheapBooks = books.Where(b => b.Price < 10);  //等同于上例中的第一个foreach区块

  foreach (var b in cheapBooks)
    Console.WriteLine(b.Title + " " + book.Price);
}

//Where方法即是在LINQ命名空间下的扩展方法(Extension Methods)，派生类Book肯定没有Where这个方法。Where方法的签名中需要传入一个返回值为布尔值的
  //委托Func<T, bool>即Predict<T>，或具有相同签名的任何方法，即传入book实例返回布尔值的方法。在这里可用匿名方法简写。

//Q: 在SQL中查询语句可以用链式的方式写，LINQ也支持么？
//A: 支持。如我们要对以上的查找结果排序，则可如下表示：

  var cheapBooks = books.Where(b => b.Price < 10).OrderBy(b = > b.Titel);

//这样就会将价格筛选结果升序排列。OrderBy()或OrderByDesciending()方法中签名须传入一个Func<T, TKey>委托或具有相同签名的方法。
//Func<T, TKey>指传入一个类，返回这个类具有的一个属性，等同KeySelector<T>，也就是需要一个排序依据，在这里按名字升序排序。

//Q: Linq中是否有SQL中的关键词SELECT和FROM？
//A: 有，功能相似但不能多选。在Linq中，Select()方法是一个类型转换方法，需要传入一个Func<T, TResult>委托或相同签名的方法，等同Selector<T>，
  //意思为它将迭代列表所有成员，除了返回所选择的属性(如title)，还会将这些成员转换为属性的类型(string)。如在以上代码中加入Select：

  var cheapBooks = books.Where(b => b.Price < 10).OrderBy(b = > b.Titel).Select(b => b.Title);

//获得的结果为以IEeumerate<string>列表储存的book title. 在实际情况中，匿名方法可能会更复杂，为了方便阅读，我们通常写成：

  var cheapBooks = books
                    .Where(b => b.Price < 10)
                    .OrderBy(b = > b.Titel)
                    .Select(b => b.Title);

//以上为使用扩展方法(Extension Methods)进行查询。

