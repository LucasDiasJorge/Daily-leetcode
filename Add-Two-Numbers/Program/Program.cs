namespace Program;

class Program
{
    static void Main(string[] args)
    {
        ListNode listNode = new ListNode(0);
        ListNode listNode2 = new ListNode(0);
        Solution solution = new Solution();
        solution.PrintListNode(solution.AddTwoNumbers(listNode, listNode2));
    }
}

public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }

    public void ReverseList(ref ListNode head)
    {
        ListNode prev = null;
        ListNode current = head;

        while (current != null)
        {
            ListNode next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }

        head = prev; // reatribui a referência do chamador
    }
}

public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int numb1 = TransformToInt(l1);
        int numb2 = TransformToInt(l2);
        int result = numb1 + numb2;
        return TransformToListNode(result);
    }

    public ListNode TransformToListNode(int numb)
    {
        ListNode head = null;
        ListNode current;
        List<int> ints = new List<int>();

        int remember = numb;
        if (numb == 0)
            ints.Add(0);
        
        while (remember != 0)
        {
            int newNumber = numb % 10;
            numb = numb / 10;
            remember = numb;
            ints.Add(newNumber);
        }

        foreach (int item in ints)
        {
            InsertFromGenesis(ref head, item);
        }

        return head;
    }

    public int TransformToInt(ListNode listNode)
    {
        int result = 0;
        int multiplier = 1;
        while (listNode != null)
        {
            result += (listNode.val * multiplier);
            multiplier *= 10;
            listNode = listNode.next;
        }
        return result;
    }

    public void InsertFromGenesis(ref ListNode? head, int val)
    {
        if (head == null)
        {
            head = new ListNode(val);
            return;
        }
        
        ListNode current = head;
        while (current.next != null)
        {
            current = current.next;
        }
        current.next = new ListNode(val);
    }

    public void PrintListNode(ListNode head)
    {
        while (head != null)
        {
            Console.WriteLine(head.val);
            head = head.next;
        }
    }
}