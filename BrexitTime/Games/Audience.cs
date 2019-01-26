using System;
using System.Collections.Generic;
using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Games
{
    public class Audience
    {
        private readonly ContentChest _contentChest;
        public System.Random _random = new System.Random();
        public List<AudienceMember> Members = new List<AudienceMember>();

        public Action<Bias> OnDecision;

        public int Rowdiness = 20;

        public Audience(ContentChest contentChest)
        {
            _contentChest = contentChest;
            for (var i = 0; i < ScreenSettings.Width; i += 20)
            {
                var spawnMember = _random.Next(0, 100) <= 80;
                if (spawnMember == false) continue;

                var selection = contentChest.AudiencePeople[_random.Next(0, contentChest.AudiencePeople.Count)];
                var c = _random.Next(0, 40);
                var bias = _random.Next(0, 100);

                bias = bias < 50 ? 0 : 1;

                var memberBias = (Bias) bias;
                UpdateAudience(memberBias);
                var mem = new AudienceMember(selection,
                    new Vector2(i, ScreenSettings.Height - selection.Height + _random.Next(0, 30)), new Color(c, c, c),
                    _random, memberBias);
                mem.OnBiasChanged += OnBiasChanged;
                Members.Add(mem);
            }
        }

        public int Brexiteers { get; private set; }
        public int Remainers { get; private set; }

        private void OnBiasChanged(Bias obj)
        {
            if (obj == Bias.Remain)
            {
                Remainers++;
                Brexiteers--;
                return;
            }

            Remainers--;
            Brexiteers++;


            if(Brexiteers == 0 || Remainers == 0)
                OnDecision?.Invoke(Brexiteers > Remainers ? Bias.Leave : Bias.Remain);
        }

        private void UpdateAudience(Bias memberBias)
        {
            switch (memberBias)
            {
                case Bias.Leave:
                    Brexiteers++;
                    break;
                case Bias.Remain:
                    Remainers++;
                    break;
            }

            Rowdiness = Math.Max(Brexiteers, Remainers);

            if (Rowdiness < Brexiteers + Remainers) return;

            Rowdiness *= 2;
        }

        public void Update(float deltaTime)
        {
            foreach (var m in Members)
                m.Update(deltaTime);


            if (_random.Next(0, 1000) < Rowdiness)
            {
                var total = Brexiteers + Remainers;
                var brexitOrRemain = _random.Next(0, total);

                if (brexitOrRemain < Brexiteers)
                {
                    var sound = _contentChest.Leaves[_random.Next(0, _contentChest.Leaves.Count)].CreateInstance();
                    sound.Volume = 0.3f;
                    sound.Pan = 0;
                    sound.Play();
                }
                else
                {
                    var sound = _contentChest.Remains[_random.Next(0, _contentChest.Remains.Count)].CreateInstance();
                    sound.Volume = 0.3f;
                    sound.Pan = 0;
                    sound.Play();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var m in Members)
                m.Draw(spriteBatch);
        }

        public void Distribute(float modifier)
        {
            foreach (var m in Members) m.UpdateBias(modifier);
        }
    }
    
}