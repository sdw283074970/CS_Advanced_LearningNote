//F: 什么是泛型(Generics)?
//A: 直接解释过于抽象，不如先理清泛型的作用，以及了解泛型被设计来是为了解决什么样的问题。

//F: 泛型有什么用？
//A: 泛型最开始要解决的是列表参数类型单一导致的实际运行效率低的问题。
//如在Arraylist中，储存的元素类型统一为object。类型object是一切对象的基类，如int, string等常见类型都是object的派生类。在实操作中，
//各种类型都能被放入到Arraylist中，如:

var al = new Arraylist();
al.Add(1);//装箱
al.Add("This is a string");//装箱
int num = (int) al[0];//拆箱+转换
string str = (string) al[1];//拆箱+转换 

//以上元素都能被作为Arraylist的元素，即使类型不同。达到这一效果所采取的操作是装箱(boxing)，装箱是指把临时空间栈(stack)中的数据类型(type)
//和值(value)打包存入永久空间的堆(heap)中，反之则成为拆箱(unboxing)。这一过程将耗费大量性能，对运行效率会产生惩罚，俗称卡。即使在Arraylist
//中全装入相同类型的元素也要经历装箱的过程，取得值还要经过拆箱转换等操作，不仅麻烦，还浪费资源。
//有没有办法在存入相同类型的元素之前先告诉系统我只会存这一种，随存随取不用装箱？于是泛型的想法孕育而生。
//通过占位符<T>来表示某一方法/函数为泛型，把T替换为任何需要用到的类型即可实现随存随取，方便且高性能。
//如典型的System.Collection.Gnerics自带的List<T>方法:

var list = new List<int>();
list.Add(1);//存入stack
list.Add(2);
int num = list[0];//从stack中取出

//另外Stack<T>也是常用的泛型，不再赘述。

//F: 那List<T>类内部长啥样？
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

//F: 可否根据需求定制自己想要的泛型类？
//A: 完全可以。以上例子其实就是一种自己定制的泛型类。以下再加个自制的Dictionary<TKey, TValue>泛型类，有所不同的是这里有两个占位符:

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

//以下为这个类的使用方法:

var dic = new GenericDictionary<string, int>();
dic.Add("name", 3);
int value = dic["name"];































