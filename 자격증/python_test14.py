class Node:
    def __init__(self, key):
        self.left = None
        self.right = None
        self.key = key

def testFunction(root):
    if root:
        testFunction(root.left)
        print(root.key, end="/")
        testFunction(root.right)

root = Node(1)
root.left = Node(5)
root.right = Node(3)
root.left.left = Node(2)
root.left.right = Node(7)
root.right.left = Node(6)
root.right.right = Node(4)

testFunction(root)