# MCUAAPI

```listen(string:key,function:fun)```

 - 감청기 등록하기
 - 유효하지 않은 key가 입력되면 등록하지 않음
 
```runcmd(string:cmd)```

 - 콘솔로서 커맨드를 실행한다.

```reNameByUuid(string:uuid,string:name)```

 - UUID의 플레이어의 이름을 재설정한다.

```selectPlayer(string:uuid)```

 - UUID로 플레이어 정보를 조회한다.

```getOnLinePlayers()```

 - 접속한 플레이어 목록을 가져온다.
 - table 타입으로 반환한다.
 - TableToJson (table) 함수로 string:Array 전환 가능하다.

```GetUUID(string:playername)```

 - 플레이어 이름으로 UUID를 가져온다.
 - 실패 시 NULL을 반환한다.

```GetXUID(string:playername)```

 - 플레이어 이름으로 XUID를 가져온다.
 - 실패 시 NULL을 반환한다.

```getscoreboard(string:uuid,string:objname)```

 - UUID의 플레이어의 scoreboard를 가져온다.
 - Objective가 존재하지 않으면 새로 생성한다.

```setCommandDescribe(string:key,string:describe)```

 - 명령어 설명을 설정한다.

```sendText(string:uuid,string:msg)```
 
 - UUID의 플레이어에게 메시지를 보낸다.

```sendSimpleForm(string:uuid,string:title,string:context,string:Array)```

 - UUID의 플레이어에게 Simple Form을 전송한다.
 - Array 형식: ["1행 내용","2행 내용,"3행 내용"]

```sendModalForm(string:uuid,string:title,string:context,string:botton1,string:botton2)```

 - UUID의 플레이어에게 Modal Form을 전송한다.
 - button1과 button2에 들어갈 텍스트를 매개변수로 받는다.

```sendCustomForm(string:uuid,string:json)```

 - UUID의 플레이어에게 커스텀 Form을 전송한다.
 - json 양식은 아래와 같다.
``` json
{
	"content": [
		{//Label
			"type": "label",
			"text": "텍스트 영역"
		},
		{//Input Box
			"placeholder": "워터마크 텍스트",
			"default": "",
			"type": "input",
			"text": ""
		},
		{//Toggle Button
			"default": true,
			"type": "toggle",
			"text": "토글~버튼"
		},
		{//Slider
			"min": 0.0,
			"max": 10.0,
			"step": 2.0,
			"default": 3.0,
			"type": "slider",
			"text": "슬라이더!!"
		},
		{//Step Slider
			"default": 1,
			"steps": [
				"Step 1",
				"Step 2",
				"Step 3"
			],
			"type": "step_slider",
			"text": "스텝 슬라이더!!"
		},
		{//Dropdown
			"default": 1,
			"options": [
				"Option 1",
				"Option 2",
				"Option 3"
			],
			"type": "dropdown",
			"text": "보는 것처럼 아래로 뻗음"
		}
	],//Custom Form
	"type": "custom_form",
	"title": "커스텀 폼 양식"
}
```

```runcmdAs(string:uuid,string:cmd)```

 - UUID의 플레이어를 주체로 명령어를 실행한다. (시뮬레이션)
 - 바닐라 명령어는 추가하고, 플러그인 명령어는 추가하지 않아도 된다.

```disconnectClient(string:uuid,string:tips)```

 - 클라이언트와 연결 끊기.
 - tips : 고지할 메시지.
 - LoadName 이벤트가 Cancel 되면 tips가 무시될 수 있다/

```addPlayerItem(string:uuid,int:itemid,int:itemaux,int:count)```

 - UUID의 플레이어에게 아이템을 지급한다.
 - 인벤토리가 꽉 찼다면 실행되지 않는다.


```talkAs(string:uuid,string:msg)```

 - UUID의 플레이어로 말한다.

```getPlayerIP(string:uuid)```

 - UUID의 플레이어의 IP를 가져온다.
