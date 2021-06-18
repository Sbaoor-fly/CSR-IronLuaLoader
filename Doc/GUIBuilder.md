# GUIBuilder
GUIBuilder는 커스텀 Form을 위한 API이다.

쉬우니 읽고 금방 구현할 수 있을 것이다.

```⛔ 이 문서는 테스트가 완료되지 않았으므로 버그 발견 시 제보바란다. (https://github.com/Sbaoor-fly/)```

```CreateGUI(string:title)```
 
 -  Form 객체를 초기화한다.
 
주의 : 이제 나오는 모든 API는 객체가 초기화 된 후 호출해야 한다.

```AddLabel(string:msg)```

 - 라벨 추가
 
```AddInput(string:msg1,string:msg2)```

 - msg1：입력 상자 이름
 
 - msg2：워터마크 글자 (html에서 placeholder 속성과 같은 기능)

```AddSlider(string:msg,int:index,int:max)```

 - 슬라이더 추가
 
 - index：초기값
 
 - max：최대값
 
```AddToggle(string:msg)```

 - 스위치 추가
 
 - msg：설명
 
```AddStepSlider(string:msg,int:index,string:Array)```

 - 스텝 슬라이더 추가
 
 - mag：설명
 
 - index：초기값
 
 - Array：목록，형식 : ["1번 값","2번 값","3번 값"]
 
 ```AddDropdown(string:msg,int:index,string:Array)```
 
  - 드롭다운 선택 상자 추가
  
  - msg：설명
  
  - index：초기값
  
  - Array：목록，형식 : ["1번 값","2번 값","3번 값"]
  
 ```SendToPlayer(string:uuid)```
 
 - uuid로 인식된 플레이어에게 전송
 
 - 폼 ID를 반환한다.
