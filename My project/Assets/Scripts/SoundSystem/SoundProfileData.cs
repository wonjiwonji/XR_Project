using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioActionType // 사용할 사운드 리스트 선언
{
    Attack,
    Button,
    BGM,
    SFX
}

[CreateAssetMenu(fileName = "Sound Profile", menuName = "Containers/SoundProfile", order = 1)]
public class SoundProfileData : ScriptableObject    // 유니티의 스크립터블 오브젝트
{
    [SerializeField] private AudioActionType audioType; // SerializeField은 스크립터블 오브젝트에서 선언한 것을 보여주는 기능
    [SerializeField] private List<AudioClip> randomClipList;

    public AudioActionType AudioType => audioType;
    public List<AudioClip> RandomClipList => randomClipList;

    public AudioClip GetRandomClip() => RandomClipList.Count > 0 ? RandomClipList.RandomItem() : null;  // 랜덤 사운드 클립 반환
    public AudioClip GetRandomIndex(int index) => RandomClipList.Count > index ? RandomClipList[index] : null;  // Index 사운드 클립 반환
}
