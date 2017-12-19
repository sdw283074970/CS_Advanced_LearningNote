//Q: 什么是可空类型？
//A: 在C#中，值类型(Value type)不能为空，但是实际情况下我们又需要值类型为空，如在数据库中，不能保证每个值类型都有值。这个时候我们就需要可空类型。
  //可能类型(Nullable class)是一个泛型类，能让不可空的值类型可空，这样一来其保存地址肯定就在堆(heap)上(个人推测，未证实)。
  
//Q: 如何在实战中使用可空类型？
//A: 有两种表示方法。第一种为Nullable<ValueType> name = null; 如：

Nullable<int> name = null;

//第二种为简便写法，即在值类型后面加问号，如：

int? name = null;

//Q: Nullable类都有什么成员？
//A: 有三个经常用到的成员：
  //1.GetValueOrDefault()方法，返回值，如果为空则返回默认值(每个类型都有自己的默认值，如int的默认值为0)；
  //2.HasValue属性，根据这个变量是否有值来返回一个布尔值；
  //3.Value属性，即保存了其值的属性，如果在为空的情况下访问这个属性则会抛出异常。
  
//Q: 如果我想把可空类型转回来怎么办？
//A: 使用其GetValueOrDefault()方法并重新赋值。

//Q: 可空合并运算符是什么？
//A: 可空合并运算符(null coalescing operator)用两个问号??表示，类似于将if else简写为 ? :，为判断可空类型专门设计，举个例子，原写法为：

static void Main(string[] args)
{
  DateTime? date1 = null;
  DateTime date;
  
  if (date1 != null)
    date = date1.GetValueOrDefault();
  else
    date = DateTime.Today;
}

//使用可空合并运算符后写作：

static void Main(string[] args)
{
  DateTime? date1 = null;
  DateTime date = date1 ?? DateTime.Today;
}

//等于说，先判定date1是否为空，不是空的话就将可空合并运算符??左边的date1赋值给date，否则将??右边的值赋给date. 语法糖。

//暂时想到这么多，最后更新2017/11/15
