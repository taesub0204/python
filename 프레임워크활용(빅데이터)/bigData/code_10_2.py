import pandas as pd
import matplotlib.pyplot as plt
from mlxtend.frequent_patterns import association_rules, apriori
from mlxtend.preprocessing import TransactionEncoder
# 데이터 준비
df = pd.read_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/chipotle.csv')

# 데이터 탐색
print(df.info(), '\n')
print(df.iloc[:, [0, 1, 2, 4]].head(10))

# 데이터 분석
print(len(df['Item'].unique()))# 중복 제거한 아이템의 개수 음식의 종류
#temp = df[df.item_price == df.item_price.max()]# 가장비싼음식
df['onePrice'] = df['item_price']/df['quantity']
df.head()
temp
temp = df[df.onePrice == df.onePrice.max()]
temp
temp = temp[['Item', 'onePrice']].drop_duplicates() # 중복 제거한 아이템의 개수 음식의 종류
temp

#temp = temp[['Item', 'onePrice']].unique() 유니크로 하면 아이템이 중복 제거 되지 않음

temp = df[df.onePrice == df.onePrice.min()]
temp
temp = temp[['Item', 'onePrice']].drop_duplicates() # 중복 제거한 아이템의 개수 음식의 종류
temp





print(len(df['Transaction'].unique()))# 트랜잭션 수 
df.tail()


#많이 판매된 음식
sales_quantity = df.groupby('Item').count()
sales_quantity = sales_quantity.sort_values('Transaction', ascending = False) #내림차순
print(sales_quantity['Transaction'], '\n')




# 매출 상위 10개 상품
top_ten = sales_quantity.sort_values('Transaction').tail(10)
top_ten = top_ten['Transaction']
top_ten


top_ten.plot.barh(xlabel = 'Transaction',
                  ylabel = '',
                  title = 'Top 10 Items',
                  figsize = (9, 5))
plt.subplots_adjust(left = 0.3)# 그래프 왼쪽 여백
plt.show()


# 연관분석 
# 전처리 
temp = df[['Transaction', 'Item']].drop_duplicates() #영수증 번호와 아이템의 중복 제거
temp = temp.groupby('Transaction')['Item'].apply(list)# 트랜잭션 번호별로 아이템을 리스트로 묶음
print(temp, '\n')

te = TransactionEncoder()
trans_matrix = te.fit(temp).transform(temp)
print(trans_matrix, '\n')

basket = pd.DataFrame(trans_matrix, columns = te.columns_)
print(basket.head(20), '\n')

# 연관규칙 탐색
freq_item = apriori(df = basket, min_support = 0.01, use_colnames = True) # 최소 지지도 1%로 설정
print(freq_item , '\n')


rules = association_rules(df = freq_item, metric = 'lift', min_threshold = 1,
                          num_itemsets = len(basket))
rules.sort_values('confidence', ascending = False, inplace = True)
rules
#rules 저장
rules.to_csv('C:/Users/user/Desktop/taesub/python/프레임워크활용(빅데이터)/data/rules.csv', index = False)


print(rules.head(10), '\n')
print(rules.iloc[0, :].transpose())