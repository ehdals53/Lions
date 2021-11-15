using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[Header("Player Behaviour SOUNDS")]
	public AudioSource _JumpSound;
	public AudioSource _DodgeSound;
	public AudioSource _BlockSound;


	[Header("Player Attack SOUNDS")]
	public AudioSource NormalAtk_voice_1;
	public AudioSource NormalAtk_voice_2;
	public AudioSource NormalAtk_voice_3;
	public AudioSource NormalAtk_voice_4;
	public AudioSource NormalAtk_voice_5;
	public AudioSource SmashAtk_voice_1;
	public AudioSource SmashAtk_voice_2;
	public AudioSource SmashAtk_voice_3;
	public AudioSource CounterAtk_voice;
	public AudioSource JumpAtk_voice;
	public AudioSource swordSound;
	public AudioSource smashSound;

	[Header("Player Hit Sound")]
	public AudioSource Lighthit_voice;
	public AudioSource Heavyhit_voice;
	public AudioSource Die_voice;


	public void NormalAtk_SwordSound()
	{
		swordSound.Play();
	}
	public void SmashAtk_SwordSound()
	{
		smashSound.Play();
	}
	public void NormalAtk_voice1()
	{
		NormalAtk_voice_1.Play();
	}
	public void NormalAtk_voice2()
	{
		NormalAtk_voice_2.Play();
	}
	public void NormalAtk_voice3()
	{
		NormalAtk_voice_3.Play();
	}
	public void NormalAtk_voice4()
	{
		NormalAtk_voice_4.Play();
	}
	public void NormalAtk_voice5()
	{
		NormalAtk_voice_5.Play();
	}
	public void SmashAtk_voice1()
	{
		SmashAtk_voice_1.Play();
	}
	public void SmashAtk_voice2()
	{
		SmashAtk_voice_2.Play();
	}
	public void SmashAtk_voice3()
	{
		SmashAtk_voice_3.Play();
	}
	public void CounterAtk_Voice()
    {
		CounterAtk_voice.Play();
    }
	public void JumpAtk_Voice()
    {
		JumpAtk_voice.Play();
    }
	public void Jump_voice()
    {
		_JumpSound.Play();
    }
	public void Dodge_voice()
    {
		_DodgeSound.Play();
    }
	public void Block_voice()
    {
		_BlockSound.Play();
    }

	public void LightHit_Voice()
    {
		Lighthit_voice.Play();

	}
	public void HeavyHit_Voice()
    {
		Heavyhit_voice.Play();
    }
	public void Die_Voice()
    {
		Die_voice.Play();
    }
}
