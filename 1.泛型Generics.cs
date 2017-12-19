//Q: 什么是泛型(Generics)?
//A: 直接解释过于抽象，不如先理清泛型的作用，以及了解泛型被设计来是为了解决什么样的问题。

//Q: 泛型有什么用？
//A: 泛型最开始要解决的是列表参数类型单一导致的实际运行效率低的问题。
  //如在Arraylist中，储存的元素类型统一为object。类型object是一切对象的基类，如int, float等常见类型都是object的派生类。在实操作中，
  //各种类型都能被放入到Arraylist中，如:

var al = new Arraylist();
al.Add(1);//装箱
al.Add("This is a string");//无装箱操作，因为string为引用型，本身储存在heap中
int num = (int) al[0];//拆箱+转换
string str = (string) al[1];//转换

//以上元素都能被作为Arraylist的元素，即使类型不同。其中采取的操作是装箱(boxing)，装箱是指把值类型(Value type)转换为引用类型(Reference type)，
  //即把临时空间栈(stack)中的数据类型(type)和值(value)打包存入永久空间的堆(heap)中，反之则成为拆箱(unboxing)。
//这一过程将耗费大量性能，对运行效率会产生惩罚，俗称卡。即使在Arraylist中全装入相同类型的元素也要经历装箱的过程，取得值还要经过拆箱转换等操作，
  //不仅麻烦，还浪费资源。
//有没有办法在存入相同类型的元素之前先告诉系统我只会存这一种，随存随取不用装箱？于是泛型的想法孕育而生。通过占位符<T>来表示某一方法/函数为泛型，
  //把T替换为任何需要用到的类型即可实现随存随取，方便且高性能。如典型的System.Collection.Gnerics自带的List<T>方法:

var list = new List<int>();
list.Add(1);//存入stack
list.Add(2);
int num = list[0];//从stack中取出

//另外Stack<T>也是常用的泛型，不再赘述。

//Q: 那List<T>类/方法内部长啥样？
//A: 以下展示仿照List<T>类及其中的Add()方法和取出值方法的构造:

Public GenericList<T> //T为占位符，占位符是什么无所谓，约定俗成为T
{
  public void Add(T value) 
  {
    //...具体方法
  }
  public T this[int index]
  {
    //...具体方法
  }
  //... 更多方法
}

//Q: 可否根据需求定制自己想要的泛型类/方法？
//A: 完全可以。以上例子其实就是一种自己定制的泛型类/方法。以下再加个自制的Dictionary<TKey, TValue>泛型类，有所不同的是这里有两个占位符:

public GenericDictionary<TKey, TValue> //若有两个以上不同类型占位符，必须区别开来，通常为T+区分关键词
{
  public void Add(TKey key, TValue value)
  {
    //...具体储存方法
  }
  public TValue this[TKey key]
  {
    //...具体方法
  }
}

//以下为这个类/方法的使用方法:

var dic = new GenericDictionary<string, int>();
dic.Add("name", 3);
int value = dic["name"];

//Q: 是否任何类型都能传递到占位符T上？
//A: 除了抽象类，任何类都能传递进去。当然我们也可以为能传入进去的类型加上人工限制。
  //为什么要加入人工限制？当然是为了更好的发挥泛型作用。没有限制的泛型在编译的时候系统默认其为object类，也就是说只能使用最原始的object那几个方法。
  //在一些情况下我们并不需要特别宽泛的泛型，比如在一个方法中，我们只想要数字类型，具体如要对传入参数进行比较，我们知道所有数字类都可以比较，但是
  //系统并不知道我们只会传入long, int, float等数字类型的值。这个时候我们就要使用关键词Where加上人工限制。以这个例子为例:

public class Untilites<T> where T : IComparable
{
  public T Max(T a, T b)
  {
    return a.CompareTo(b) > 0 ? a : b;
  }
}

//意思为T类型为所有有IComparable接口的类。另外列出所有五种类型的限制：
  //1. 限制占位类为有该接口的类。如 Where T : IComparable(任何接口)
  //2. 限制占位类为某一特定类和该类的子类的类。如 Where T : Product(任何指定的类)
  //3. 限制占位类为值类型(Value type)。仅 Where T : struct
  //4. 限制占位类为引用类型(Reference type)。仅 Where T : class
  //5. 限制占位类为有默认构造器的类。仅 Where T : new()
//详细解释下第五种情况。在有些时候我们需要对占位类T实例化，但是系统并不知道这是个类如何具体实例化，需要传递哪些参数。这个时候如果该占位类有默认构造器
  //那么就可以实例化，所以需要加上此限制new()，有默认构造器的占位类系统才知道怎么将它实例化。接着以上面例子为例:

public class Untilites<T> where T : IComparable, new()
{
  public T Max(T a, T b)
  {
    return a.CompareTo(b) > 0 ? a : b;
  }
  
  public void Weried(T t)
  {
    var obj = new T();
  }
}

//以上就想到这么多，以后再补充。最后更新2017年11月09日。
