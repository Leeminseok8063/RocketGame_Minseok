using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Challenge
{
    internal class Initiallizer : MonoBehaviour
    {
        private void Start()
        {
            SoundManger.Instance.Init();
        }
    }
}
