using UnityEngine;

namespace schw3de.ld48
{
    public static class SpriteRendererExtension
    {
        public static SpriteRenderer SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            var tmpColor = spriteRenderer.color;
            tmpColor.a = alpha;
            spriteRenderer.color = tmpColor;
            return spriteRenderer;
        }

        public static bool SubstractAlpha(this SpriteRenderer spriteRenderer, float substractAlpha)
        {
            if(spriteRenderer.color.a == 0)
            {
                return false;
            }

            var tmpColor = spriteRenderer.color;
            var newAlphaValue = tmpColor.a - substractAlpha;
            if(newAlphaValue <= 0.01f)
            {
                newAlphaValue = 0;
            }
            tmpColor.a = newAlphaValue;
            spriteRenderer.color = tmpColor;
            return true;
        }

        public static bool AddAlpha(this SpriteRenderer spriteRenderer, float addAlpha)
        {
            if(spriteRenderer.color.a == 100)
            {
                return false;
            }

            var tmpColor = spriteRenderer.color;
            var newAlphaValue = tmpColor.a + addAlpha;
            if(newAlphaValue >= 0.99f)
            {
                newAlphaValue = 1;
            }
            tmpColor.a = newAlphaValue;
            spriteRenderer.color = tmpColor;
            return true;
        }

    }
}
