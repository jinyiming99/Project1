using System;
using JetBrains.Annotations;

namespace GameFrameWork.RequirementSystem
{
    public interface IRequirement
    {
        
    }
    public abstract class Requirement<T> : IComparable<Requirement<T>>,IRequirement where T :IComparable<T>
    {
        protected T m_arg1;

        public T Arg1 => m_arg1;

        public Requirement()
        {
            
        }
        public Requirement(T arg)
        {
            m_arg1 = arg;
        }
        
        public static bool operator ==(Requirement<T> left,Requirement<T> right)
        {
            return left.Arg1.CompareTo(right.m_arg1) == 0;
        }
        public static bool operator !=(Requirement<T> left,Requirement<T> right)
        {
            return left.Arg1.CompareTo(right.m_arg1) != 0;
        }

        public int CompareTo(Requirement<T> other)
        {
            if (other == null)
                return 1;
            return Arg1.CompareTo(other.Arg1);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Requirement<T> data)
                return this == data;
            return false;
        }

        public abstract int GetHashCode();

    }
    
    public abstract class Requirement<T,U> :Requirement<T>,IComparable<Requirement<T,U>> where T :IComparable<T>
                                        where U:IComparable<U>
    {

        protected U m_arg2;
        public U Arg2 => m_arg2;

        public Requirement(T arg ,U arg2):base(arg)
        {
            m_arg2 = arg2;
        }

        public static bool operator ==(Requirement<T,U> left,Requirement<T,U> right)
        {
            return left.Arg1.CompareTo(right.Arg1) == 0&& left.Arg2.CompareTo(right.Arg2) == 0;
        }
        public static bool operator !=(Requirement<T,U> left,Requirement<T,U> right)
        {
            return left.Arg1.CompareTo(right.Arg1) != 0 || left.Arg2.CompareTo(right.Arg2) != 0;
        }
        public int CompareTo(Requirement<T,U> other)
        {
            return Arg1.CompareTo(other.Arg1) + Arg2.CompareTo(other.Arg2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Requirement<T,U> data)
                return this == data;
            return false;
        }
    }
}