
# JLib 자주 사용하는 기능들을 구현해놓음
## 설치
Unity 2023.3.4f1에서 작동 확인됨

<img src="https://github.com/jjr2930/JLib/blob/master/Documentation~/how%20to%20install%20step1.png"/>
<img src="https://github.com/jjr2930/JLib/blob/master/Documentation~/how%20to%20install%20step2.png"/>
<img src="https://github.com/jjr2930/JLib/blob/master/Documentation~/how%20to%20install%20step3.png"/>

## Tween
<img width=600 height=400 src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/Tween.gif?raw=true"/>

## Common Popup UI (yes or no팝업)

<img width=600 height=500 src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/CommonPopupUI.png?raw=true"/>

## Localization Text (현지화 텍스트)
LocalizationTable Inspector<p>
<img width=600 height=400 src="https://github.com/jjr2930/JLib/blob/master/Documentation~/LocalizationTextTableInspector.png?raw=true"/>

LocalizationTextTMP inspector<p>
<img width=600 height=800 src ="https://github.com/jjr2930/JLib/blob/master/Documentation~/LocalizationTextTMP%20Inspector.png?raw=true"/>

동작<p>
<img width=600 height=400 src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/LocalizationTable.gif?raw=true"/>

## Object Pool

<img src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/Object%20Pool.gif?raw=true"/>

class diagram
<img src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/ObjectPool%20%ED%81%B4%EB%9E%98%EC%8A%A4%20%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8.png?raw=true"/>

## Simple State Machine
<img Width=400 height=750 src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/StateMachine%20Inspector.png?raw=true"/>

## SubScene
<img width=800 height=400 src="https://github.com/jjr2930/JLib/blob/master/Documentation~/subscene.png?raw=true"/>


- 에디터 모드와 플레이 모드에서 모두 사용가능한 모듈
- 작업을 할 때에 씬별로 작업하는 경우가 많다.
- 하지만 이렇게 작업한 요소들이 한번에 모두 보여져야 하는 경우가 있다.
- scene additive를 사용하면 여러개의 씬을 보여줄 수 있지만, 에디터에서만 동작을 한다거나, 런타임에만 사용이 가능한다거나 등과 같은 문제가 있다.
- 이 컴포넌트는 원하는 scene을 인스펙터에 등록하고, loadscene버튼을 누르면 씬이 추가되어 위에서 설명한 작업이 자동으로 동작하게 한다.
- 추후 load scene 버튼을 누르지 않아도 이 컴포넌트가 씬에 존재하면 자동을 씬으로 불러오게 하는 기능을 만들것이다.




## Favorite items window
<img width=400 height=750 src = "https://github.com/jjr2930/JLib/blob/master/Documentation~/favorite%20items.png?raw=true"/>

- 자주 사용하는 asset(prefab, scene, model, sprites 등)을 등록하여, 선택하거나 더블 클릭이 가능하도록 하는 기능이다.
- local config에만 저장되기에 다른 사람과의 즐겨찾기 충돌은 없다.
