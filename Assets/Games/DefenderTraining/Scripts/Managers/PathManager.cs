using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.DefenderTraining.Scripts.Managers
{
    public class PathManager : MonoBehaviour
    {
        private static PathManager _instance;
        private List<Transform> _paths;

        public static PathManager Instance => _instance;

        public List<Transform> Paths
        {
            get => _paths;
            set => _paths = value;
        }

        private void Awake()
        {
            if (_instance != this && _instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _paths = new List<Transform>();
            var rawPaths = GameObject.FindGameObjectsWithTag("Path");
            Array.Sort(rawPaths, (x, y) => String.Compare(x.name, y.name, StringComparison.Ordinal));
            foreach (var path in rawPaths)
            {
                _paths.Add(path.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
