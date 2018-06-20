using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class MyStack<T> {

    T[] StackArray;
    int StackPointer = 0;

    const int maxStack = 10;
    bool IsStackFull { get { return StackPointer >= maxStack; } }
    bool IsStackEmpty { get { return StackPointer <= 0; } }



    public void Push(T x)
    {
        if (!IsStackFull) {
            StackArray[StackPointer++] = x; }
    }

    public T Pop()
    {
        return (!IsStackEmpty)
            ? StackArray[--StackPointer]
            :StackArray[0];
        
    }

    public MyStack()
    {
        StackArray = new T[maxStack];
    }


    public void Print()
    {
        for(int i = StackPointer - 1; i >= 0; i--)
        {
            Console.WriteLine("  Value : {0}", StackArray[i]);
        }
    }



}

class Program
{
    static void Main()
    {
        MyStack<int> StackInt = new MyStack<int>();
        MyStack<string> StackString = new MyStack<string>();

        StackInt.Push(3);
        StackInt.Push(4);
        StackInt.Push(5);
        StackInt.Push(6);
        StackInt.Print();

        StackString.Push("Wheels Are Awesome!");
        StackString.Push("20180620");
        StackString.Print();

    }




}
