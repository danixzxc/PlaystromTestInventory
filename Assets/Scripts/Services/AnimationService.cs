using DG.Tweening;
using UnityEngine;
using System;

namespace Services
{
    public class AnimationService
    {
        private readonly float _duration = 0.5f;
        private readonly float _arcHeight = 2f;
        private readonly Ease _ease = Ease.OutCubic;

        public void FlyToTarget(Transform item, RectTransform uiTarget, Action onComplete)
        {
            Vector3 startPosition = item.position;
            Vector3 targetWorldPosition = uiTarget.position;
            Vector3 midPoint = (startPosition + targetWorldPosition) / 2f;
            midPoint.y += _arcHeight;

            Vector3[] path = new Vector3[]
            {
                startPosition,
                midPoint,
                targetWorldPosition
            };

            item.DOPath(path, _duration, PathType.CatmullRom)
                .SetEase(_ease)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });

            item.DOScale(Vector3.zero, _duration * 0.3f).SetDelay(_duration * 0.7f);
        }
    }
}