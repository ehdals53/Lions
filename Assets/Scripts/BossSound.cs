using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    [Header("Boss Attack Sound")]
    public AudioSource Boss_Attack_voice1;
    public AudioSource Boss_Attack_voice2;
    public AudioSource Boss_Attack_voice3;
    public AudioSource Boss_Attack_voice4;
    public AudioSource Boss_Attack_voice5;
    public AudioSource Boss_Attack_voice6;
	public AudioSource BOss_Die_voice;

	public void BOSS_ATTACK_VOICE1()
	{
		Boss_Attack_voice1.Play();
	}
	public void BOSS_ATTACK_VOICE2()
	{
		Boss_Attack_voice2.Play();
	}
	public void BOSS_ATTACK_VOICE3()
	{
		Boss_Attack_voice3.Play();
	}
	public void BOSS_ATTACK_VOICE4()
	{
		Boss_Attack_voice4.Play();
	}
	public void BOSS_ATTACK_VOICE5()
	{
		Boss_Attack_voice5.Play();
	}
	public void BOSS_ATTACK_VOICE6()
	{
		Boss_Attack_voice6.Play();
	}
	public void BOSS_DIE_VOICE()
    {
		BOss_Die_voice.Play();
    }
}
