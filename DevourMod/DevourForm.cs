using MelonLoader;
using Photon.Bolt;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using UnityEngine;

namespace DevourMod
{
    public partial class DevourForm : Form
    {
        public DevourForm()
        {
            InitializeComponent();
        }

        private void DevourForm_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/ImStellar/DevourMod");
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            DevourMain.freefall = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            DevourMain.rankspoof = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            DevourMain.spinbot = checkBox4.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // I got this from a person named Trey
            MelonLogger.Msg("Unlocked all achievements");
            try
            {
                var ah = UnityEngine.Object.FindObjectOfType<AchievementHelpers>();
                string[] names = { "hasAchievedFusesUsed", "hasAchievedGasolineUsed", "hasAchievedNoKnockout", "hasCollectedAllPatches", "hasCollectedAllRoses",
                "hasCompletedHardAsylumGame", "hasCompletedHardGame", "hasCompletedNightmareAsylumGame", "hasCompletedNightmareGame", "hasCompletedNormalGame",
                "isStatsValid", "isStatsFetched", "hasCompletedHardInnGame", "hasCompletedNightmareInnGame" };
                object[] values = { true, true, true, true, true, true, true, true, true, true, true, true, true, true };

                ReplaceMultipleFields(ah, names, values, BindingFlags.Instance | BindingFlags.NonPublic);

                string[] achievments = { "ACH_ALL_ROSES", "ACH_BURNT_GOAT", "ACH_SURVIVED_TO_3_GOATS", "ACH_SURVIVED_TO_5_GOATS", "ACH_SURVIVED_TO_7_GOATS", "ACH_WON_SP", "ACH_WON_COOP",
                "ACH_LOST", "ACH_LURED_20_GOATS", "ACH_REVIVED_20_PLAYERS", "ACH_ALL_NOTES_READ", "ACH_KNOCKED_OUT_BY_ANNA", "ACH_KNOCKOUT_OUT_BY_DEMON", "ACH_KNOCKED_OUT_20_TIMES",
                "ACH_NEVER_KNOCKED_OUT", "ACH_ONLY_ONE_KNOCKED_OUT", "ACH_UNLOCKED_CAGE", "ACH_UNLOCKED_ATTIC_CAGE", "ACH_BEAT_GAME_5_TIMES", "ACH_100_GASOLINE_USED",
                "ACH_FRIED_20_DEMONS", "ACH_STAGGERED_ANNA_20_TIMES", "ACH_CALMED_ANNA_10_TIMES", "ACH_CALMED_ANNA", "ACH_WIN_NIGHTMARE", "ACH_BEAT_GAME_5_TIMES_IN_NIGHTMARE_MODE",
                "ACH_WON_NO_KNOCKOUT_COOP", "ACH_WIN_NIGHTMARE_SP", "ACH_WON_HARD", "ACH_WON_HARD_SP", "ACH_100_FUSES_USED", "ACH_ALL_CLIPBOARDS_READ", "ACH_ALL_PATCHES",
                "ACH_FRIED_RAT", "ACH_FRIED_100_INMATES", "ACH_LURED_20_RATS", "ACH_STAGGERED_MOLLY_20_TIMES", "ACH_WON_MOLLY_SP", "ACH_WON_MOLLY_HARD_SP", "ACH_WON_MOLLY_NIGHTMARE_SP",
                "ACH_WON_MOLLY_COOP", "ACH_WON_MOLLY_HARD", "ACH_WON_MOLLY_NIGHTMARE", "ACH_20_TRASH_CANS_KICKED", "ACH_CALM_MOLLY_10_TIMES", "ACH_WON_INN_HARD", "ACH_WON_INN_HARD_SP", "ACH_WON_INN_NIGHTMARE", "ACH_WON_INN_NIGHTMARE_SP", "ACH_WON_INN_COOP", "ACH_100_EGGS_DESTROYED", "ACH_ALL_CHERRY_BLOSSOM" };
                for (int i = 0; i < achievments.Length; i++)
                {
                    ah.Unlock(achievments[i]);
                }
            }
            catch { }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ReplaceMultipleFields(object obj, string[] names, object[] values, BindingFlags bf)
        {
            for (int i = 0; i < names.Length; i++)
            {
                obj.GetType().GetProperty(names[i], bf).SetValue(obj, values[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Opsive.UltimateCharacterController.Traits.Respawner respaw in Resources.FindObjectsOfTypeAll<Opsive.UltimateCharacterController.Traits.Respawner>())
                {
                    respaw.Respawn();
                }
            }
            catch { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GoatBehaviour g = UnityEngine.Object.FindObjectOfType<GoatBehaviour>();
                for (int i = 0; i < 200; i++)
                {
                    BoltNetwork.Instantiate(g.entity.PrefabId, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity), DevourMain.LocalPlayer.transform.rotation);
                    g.gameObject.SetActive(false);
                }
            }
            catch { }
        }
    }
}