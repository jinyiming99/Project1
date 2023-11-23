using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public interface IExpressionComponent
    {
        void Do(UIComponentStates state);
    }
    [System.Serializable]
    public abstract class CodeExpressionBase<T> :IExpressionComponent
    {
        public float duration;
        public Graphic graphic;
        public T normalValue;
        public T hightLightValue;
        public T pressValue;
        public T disableValue;
        public T selectedValue;

        protected T GetValue(UIComponentStates states)
        {
            switch (states)
            {
                case UIComponentStates.normal:
                    return normalValue;
                case UIComponentStates.press:
                    return pressValue;
                case UIComponentStates.hightLight:
                    return hightLightValue;
                case UIComponentStates.disable:
                    return disableValue;
            }

            return default(T);
        }

        public abstract void Do(UIComponentStates states);
    }
    [System.Serializable]
    public class ScaleAnimation : CodeExpressionBase<Vector3>
    {
        public ScaleAnimation()
        {
            duration = 0.15f;
            normalValue = Vector3.one;
            hightLightValue = Vector3.one;
            pressValue = Vector3.one;
            disableValue = Vector3.one;
            selectedValue = Vector3.one;
        }
        public override void Do(UIComponentStates states)
        {
            if (graphic != null)
                graphic.transform.DOScale(GetValue(states), duration);
        }
    }
    [System.Serializable]
    public class ColorAnimation : CodeExpressionBase<Color>
    {
        public ColorAnimation()
        {
            duration = 0.15f;
            normalValue = Color.white;
            hightLightValue = Color.white;
            pressValue = Color.white;
            disableValue = Color.white;
            selectedValue = Color.white;
        }
        public override void Do(UIComponentStates states)
        {
            if (graphic != null)
                graphic.DOColor(GetValue(states), duration).SetEase(Ease.Linear);
        }
    }
    [System.Serializable]
    public class OffsetAnimation: CodeExpressionBase<Vector3>
    {
        public OffsetAnimation()
        {
            duration = 0.15f;
            normalValue = Vector3.zero;
            hightLightValue = Vector3.zero;
            pressValue = Vector3.zero;
            disableValue = Vector3.zero;
            selectedValue = Vector3.zero;
        }
        public override void Do(UIComponentStates states)
        {
            if (graphic != null)
                graphic.transform.DOLocalMove(GetValue(states), duration);
        }
    }
    [System.Serializable]
    public class UIExpressionComponent
    {

        public ScaleAnimation[] _scaleAnimations;

        public ColorAnimation[] _colorAnimations;

        public OffsetAnimation[] _offsetAnimations;
        
        public void OnStateChange(UIComponentStates state)
        {
            DoExpression(_scaleAnimations, state);
            DoExpression(_colorAnimations, state);
            DoExpression(_offsetAnimations, state);
        }

        private void DoExpression(IExpressionComponent[] array, UIComponentStates state)
        {
            if (array == null)
                return;
            foreach (var expressionComponent in array)
            {
                expressionComponent.Do(state);
            }
        }
        
    }
}