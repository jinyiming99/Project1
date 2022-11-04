using System;
using GameFrameWork.RequirementSystem;

namespace GameFrameWork
{
    public class ResourceRequirement : Requirement<string> 
    {
        public ResourceRequirement(string str) : base(str)
        {
            
        }

        public override int GetHashCode()
        {
            return m_arg1.GetHashCode();
        }
    }
}