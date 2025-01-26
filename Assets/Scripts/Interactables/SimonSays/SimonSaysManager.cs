    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SimonSaysManager : MonoBehaviour
    {
        public static bool completed = false;
        
        public PingToPlaySimonSaysSequence answer;
        public List<PingSimonSaysElement> sequence;
        
        private readonly List<PingSimonSaysElement> currentlyPlayedOrder = new List<PingSimonSaysElement>();
        
        private void OnEnable()
        {
            foreach(var child in sequence)
                child.Pinged += OnPinged;
        }

        private void OnDisable()
        {
            foreach(var child in sequence)
                child.Pinged -= OnPinged;
        }

        private void OnPinged(PingSimonSaysElement element)
        {
            currentlyPlayedOrder.Add(element);
            
            if(currentlyPlayedOrder.Count == sequence.Count)
            {
                if(currentlyPlayedOrder.SequenceEqual(sequence))
                {
                    answer.PlaySuccess();
                    completed = true;
                }
                else
                {
                    answer.PlayFail();
                }
                
                currentlyPlayedOrder.Clear();
            }
        }
    }