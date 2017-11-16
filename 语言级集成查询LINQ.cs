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
  Var books = new BookRepository().GetBooks();  //返回的是一个IEnumerable<T>列表
  
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
//Func<T, TKey>指传入一个类，返回这个类具有的一个属性，也就是需要一个排序依据，在这里按名字升序排序。

//Q: Linq中是否有SQL中的关键词SELECT和FROM？
//A: 有，功能相似但不能多选。在Linq中，Select()方法是一个类型转换方法，需要传入一个Func<T, TResult>委托或相同签名的方法，
  //意思为它将迭代列表所有成员，除了返回所选择的属性(如title)，还会将这些成员转换为属性的类型(string)。如在以上代码中加入Select：

  var cheapBooks = books.Where(b => b.Price < 10).OrderBy(b = > b.Titel).Select(b => b.Title);

//获得的结果为以IEeumerable<string>列表储存的book title. 在实际情况中，匿名方法可能会更复杂，为了方便阅读，我们通常写成：

  var cheapBooks = books
                    .Where(b => b.Price < 10)
                    .OrderBy(b = > b.Titel)
                    .Select(b => b.Title);

//以上为使用扩展方法(Extension Methods)进行查询。
//我们还能使用SQL风格的查询语句，但是顺序跟原版不太一样，如下：

  var cheapBooks = from b in books
                    where b < 10
                    orderBy b.Titel
                    select b.Title;

//注意这两种写法的区别，SQL风格的语句为全小写。两种方法除了语法不同以外，产生的结果完全一样，具体用哪一种风格全凭个人喜好。

//Q: 除了以上涉及到的扩展方法，还有没有一些其他实用的Linq扩展方法介绍？
//A: Single()方法。Where()方法返回的是IEnumerable<T>列表，如果只用放回一个对象的话就用Single()方法，使用方法与Where()差不多，都是传入一个签
  //名为(Book book)、返回值为bool的方法或等价的匿名方法(Predict<T>委托类型)。但是如果查找对象中没有满足匿名方法条件的对象时，控制台会抛出异常。
  //为了修复这个问题，在不清楚是否有能够匹配的对象的时候，我们使用SingleOrDefault()方法来代替。区别在于，SingleOrDefault()方法在没找到对象的时
  //候，会返回默认值，默认情况下默认值为null；
//First()方法，即返回第一个满足条件的对象。同样我们有FirstOrDefault；
//Skip(n).Take(m)方法，两个连着一起用,指跳过n个满足对象的条目，取其后m个满足对象的条目，并返回为一个IEnumerable<T>列表；
//Count()方法。返回查询目标的条目总数；
//Max()方法、Min()方法。返回在泛型次序中较大的/小的那一个数；
//Sum()方法。返回泛型次序中数值属性的和。
//Last()方法。返回最后一个满足条件的对象，同样我们有LastOrDefault；
//Average()方法。返回泛型次序中数值属性的平均值。

//以上就是Linq基础部分。
//暂时想到这么多，最后更新2017/11/15
