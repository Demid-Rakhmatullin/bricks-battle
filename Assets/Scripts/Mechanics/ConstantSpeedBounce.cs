using UnityEngine;

namespace BricksBattle.Mechanics
{
    /// <summary>
    /// Данная механика решает проблемы с поддержанием постоянной скорости мяча. Импульс мячу придается один раз на старте, и в текущей реализации должен сохраняться.
    /// Проблем с сохранением импульса было выявлено две.
    /// 
    /// 1. Постепенное замедление мяча. Небольшое замедление происходит при отскоке мяча от платформы и со временем накапливается.
    /// Насколько я разобрался, это связано с тем, что при столкновении с другим dynamic rigidbody мяч толкает его в сторону удара и тратит на это часть энергии.
    /// То, что у rigidbody платформы есть freeze position по оси Y в полной мере проблему не решает. 
    /// Текущая гипотеза в том, что unity все равно считает симуляцию и при толкании по freeze position, а потом как бы упирает объект в невидимую стену по заблокированной оси.
    /// Нулевой friction и единичный bounciness у материала мяча также проблему не решают, т.к. эти настройки не отключают физику "толкания" других dynamic rigidbody при ударе.
    /// 
    /// 2. Резкое ускорение мяча при его отбитии углом или боковой гранью платформы. 
    /// Происходит, если в момент отскока платформа также находилась в движении, и направление движения плаформы близко к направлению отскока.
    /// Как я понимаю, в этом случае уже сама платформа отдает часть своей энергии движения мячу и ускоряет его.
    /// Также иногда заметен и обратный эффект, когда мяч сдвигает платформу по оси X при попадании в боковые грани. Этот кейс пока оставлен как есть, т.к. такого сильного влияения на пользователький опыт не оказывает.
    /// </summary>
    public class ConstantSpeedBounce
    {
        private readonly Rigidbody2D rigidbody;
        private float previousSpeed;

        public ConstantSpeedBounce(Rigidbody2D rigidbody)
        {
            this.rigidbody = rigidbody;
            previousSpeed = -1f;
        }

        public void SaveSpeed()
        {
            previousSpeed = rigidbody.linearVelocity.magnitude;
        }

        public void ProcessCollision(Collision2D collision)
        {
            if (collision.rigidbody &&
                collision.rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 currentDirection = rigidbody.linearVelocity.normalized;
                rigidbody.linearVelocity = currentDirection * previousSpeed;
            }
        }
    }
}
