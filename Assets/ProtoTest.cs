using UnityEngine;
using System.Collections;
using Bufan.Proto;
using ProtoData;
using System.IO;
using UnityEngine.UI;

public class ProtoTest : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
        Person p = new Person();
        p.id = 1;
        p.name = "张三";
        p.email = "123456@163.com";
        byte [] array = ProtobufTool.ProtoBufToBytes<Person>(p);
        Person tp = ProtobufTool.BytesToProtoBuf<Person>(array);
        string pData = tp.id + " " + tp.name + " " + tp.email;
        print(pData);
        text.text = pData + '\n' + SystemInfo.deviceUniqueIdentifier;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
