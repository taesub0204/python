# pivot_table 오류 해결 정리

## 문제 상황
다음 코드에서 실행 오류가 발생했다.

```python
pivot_df = df.pivot_table(
    index = 'lot_id',
    columns = 'run_status',
    values = 'run_id',
    aggfunc = 'count'
)
```

## 원인
오류 원인은 `pivot_table` 문법이 아니라, `df` 안에 `lot_id` 컬럼이 없었기 때문이다.

즉, `index='lot_id'`로 묶으려 했지만, 이전 SQL 조회 결과에 `lot_id`를 가져오지 않아서 `KeyError: 'lot_id'`가 발생했다.

## 해결 방법
`df`를 만드는 SQL 쿼리에 `r.lot_id`를 추가하면 된다.

```python
query = """
select 
    r.run_id,
    e.equipment_id,
    e.model_name,
    p.step_name,
    p.process_group,
    r.run_status,
    r.start_time,
    r.lot_id
from runhistory as r
join equipment as e on r.equipment_id = e.equipment_id
join processstep as p on r.step_id = p.step_id;
"""

df = pd.read_sql(query, conn)
```

그다음 pivot_table을 실행하면 정상 동작한다.

```python
pivot_df = df.pivot_table(
    index = 'lot_id',
    columns = 'run_status',
    values = 'run_id',
    aggfunc = 'count'
)
```

## 결과
lot_id 기준으로 run_status별 run_id 개수가 집계된다.
