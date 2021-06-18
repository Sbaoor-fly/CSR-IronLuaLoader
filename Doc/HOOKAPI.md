# HOOKAPI

```teleport(string:uuid,int:x,int:y,int:z,int:did)```

 - 마인크래프트 버전에 맞춰 Hook를 업데이트 해야한다.
 - 청크가 로딩되지 않은 곳에 TP하면 죽을 수도 있음.
 
 
 
# 자체 후킹 하는 법
## 1. 상대 메모리 주소(RVA) 찾기

bedrock_server.pdb에서 다음 기호를 검색한다.

```?teleport@TeleportCommand@@SAXAEAVActor@@VVec3@@PEAV3@V?$AutomaticID@VDimension@@H@@VRelativeFloat@@4HAEBUActorUniqueID@@@Z```

그러면 사진과 같은 정보를 확인할 수 있다.

![NPP 图标](https://raw.githubusercontent.com/Sbaoor-fly/CSR-IronLuaLoader/master/Doc/pic/nppp-search.png)

## 2. 적용하기

솔루션 파일 236행에 있는 버전과, 찾은 주소를 입력한다.

```cs
{BDS버전, 메모리 주소 }
```
예：1.16.210.06 버전에서는 아래와 같이 한다.
```cs
{"1.16.210.06", 0x007B1D20 }
```

## 3. 번역본