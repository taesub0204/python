def process_data(data_list):
    sliced_data = data_list[1:4]
    processed_list = []
    for item in sliced_data:
        processed_list.append(item+ '-Checked')
    return processed_list
data = ['a', 'b', 'c', 'd', 'e']
result1 = process_data(data)
result2 = result1[1].split('e')
print(result2[1])