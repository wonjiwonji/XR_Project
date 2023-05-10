using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioActionType // ����� ���� ����Ʈ ����
{
    Attack,
    Button,
    BGM,
    SFX
}

[CreateAssetMenu(fileName = "Sound Profile", menuName = "Containers/SoundProfile", order = 1)]
public class SoundProfileData : ScriptableObject    // ����Ƽ�� ��ũ���ͺ� ������Ʈ
{
    [SerializeField] private AudioActionType audioType; // SerializeField�� ��ũ���ͺ� ������Ʈ���� ������ ���� �����ִ� ���
    [SerializeField] private List<AudioClip> randomClipList;

    public AudioActionType AudioType => audioType;
    public List<AudioClip> RandomClipList => randomClipList;

    public AudioClip GetRandomClip() => RandomClipList.Count > 0 ? RandomClipList.RandomItem() : null;  // ���� ���� Ŭ�� ��ȯ
    public AudioClip GetRandomIndex(int index) => RandomClipList.Count > index ? RandomClipList[index] : null;  // Index ���� Ŭ�� ��ȯ
}
