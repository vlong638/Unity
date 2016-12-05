using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.VL.Scripts
{
    public class Wall : MonoBehaviour
    {
        public Sprite damageSprite;
        public int hp = 4;
        public AudioClip chopSound1;
        public AudioClip chopSound2;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DamageWall(int loss)
        {
            SoundManager.instance.Play(chopSound1, chopSound2);
            spriteRenderer.sprite = damageSprite;
            hp -= loss;
            if (hp <= 0)
                gameObject.SetActive(false);
        }
    }
}
