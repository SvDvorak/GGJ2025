    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
    using UnityEngine;

    public class SimonSaysManager : MonoBehaviour
    {
        public static SimonSaysManager Instance;
        
        public bool completed = false;
        
        public PingToPlaySimonSaysSequence answer;
        public List<PingSimonSaysElement> sequence;
        public List<ViewDirectionShading> correctGlows;
        
        private readonly List<PingSimonSaysElement> currentlyPlayedOrder = new List<PingSimonSaysElement>();
        
        private void OnEnable()
        {
            Instance = this;
            foreach(var child in sequence)
                child.Pinged += OnPinged;
        }

        private void Start()
        {
            foreach(var child in correctGlows)
                child.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            foreach(var child in sequence)
                child.Pinged -= OnPinged;
        }

        private void OnPinged(PingSimonSaysElement element)
        {
            if(currentlyPlayedOrder.Count == sequence.Count)
                currentlyPlayedOrder.RemoveAt(0);
            
            currentlyPlayedOrder.Add(element);

            // We essentially check from the latest input going backwards to see if the sequence is correct so far
            // If so, we then play the light for that part in the sequence
            var current = sequence.IndexOf(element);
            var correctSoFar = true;

            if(current >= currentlyPlayedOrder.Count)
                // We played a totem further along in the sequence
                return;

            for(var i = 1; i <= current; i++)
            {
                var previousCorrect = sequence[current - i];
                var previousInput = currentlyPlayedOrder[currentlyPlayedOrder.Count - 1 - i];
                if(previousInput != previousCorrect)
                    correctSoFar = false;
            }
            
            if(correctSoFar)
            {
                PlayGlow(current);
                
                if(current == sequence.Count - 1)
                {
                    answer.PlaySuccess();
                    completed = true;
                }
            }
        }

        public void PlayGlow(int index)
        {
            var currentGlow = correctGlows[index];
            currentGlow.gameObject.SetActive(true);
            currentGlow.FadeOut(0);
            DOTween.Sequence()
                .Append(currentGlow.FadeIn(0.3f))
                .AppendInterval(0.5f)
                .Append(currentGlow.FadeOut(0.4f))
                .AppendCallback(() => currentGlow.gameObject.SetActive(false));
        }
    }