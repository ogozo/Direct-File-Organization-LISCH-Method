using System;
class CoalescedHashTable
{
    class HashNode
    {
        public int Key { get; set; }
        public HashNode Next { get; set; }
        public HashNode(int key)
        {
            Key = key;
            Next = null;
        }
    }

    private HashNode[] table;
    private int size;
    public CoalescedHashTable(int size)
    {
        this.size = size;
        table = new HashNode[size];
    }
    private int HashFunction(int key)
    {
        return key % size;
    }
    public void Insert(int key)
    {
        int homeAddress = HashFunction(key);

        if (table[homeAddress] == null)
        {
            table[homeAddress] = new HashNode(key);
        }
        else
        {
            HashNode current = table[homeAddress];

            while (current != null)
            {
                if (current.Key == key)
                {
                    Console.WriteLine("Duplicate record with key {0}", key);
                    return;
                }

                if (current.Next == null)
                {
                    int emptyLocation = FindEmptyLocation();
                    if (emptyLocation == -1)
                    {
                        Console.WriteLine("Full table");
                        return;
                    }
                    table[emptyLocation] = new HashNode(key);
                    current.Next = table[emptyLocation];
                    return;
                }

                current = current.Next;
            }
        }
    }
    private int FindEmptyLocation()
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (table[i] == null)
            {
                return i;
            }
        }

        return -1;
    }
    public int Search(int key)
    {
        int homeAddress = HashFunction(key);
        int probes = 0;

        HashNode current = table[homeAddress];

        while (current != null)
        {
            probes++;

            if (current.Key == key)
            {
                return probes;
            }

            current = current.Next;
        }

        return -1;
    }
    public void PrintTable()
    {
        for (int i = 0; i < size; i++)
        {
            if (table[i] == null)
            {
                Console.WriteLine("Index{0}: NULL", i);
            }
            else
            {
                if (table[i].Next != null)
                {
                    Console.WriteLine("Index{0}: {1} -> Link to Index{2}", i, table[i].Key, GetNodeIndex(table[i].Next));
                }
                else
                {
                    Console.WriteLine("Index{0}: {1}", i, table[i].Key);
                }
            }
        }
    }
    private int GetNodeIndex(HashNode node)
    {
        for (int i = 0; i < size; i++)
        {
            if (table[i] == node)
            {
                return i;
            }
        }

        return -1;
    }
    public void HowManyProbes(CoalescedHashTable hashTable)
    {
        Console.Write("\nSearch for: ");
        int keyToSearch = Convert.ToInt32(Console.ReadLine());
        int probes = hashTable.Search(keyToSearch);

        if (probes != -1)
        {
            Console.WriteLine($"Key {keyToSearch} found in {probes} probes.");
        }
        else
        {
            Console.WriteLine($"Key {keyToSearch} not found.");
        }
    }
}
class Program
{

    static void Main()
    {
        CoalescedHashTable hashTable = new CoalescedHashTable(11);

        hashTable.Insert(27);
        hashTable.Insert(18);
        hashTable.Insert(29);
        hashTable.Insert(28);
        hashTable.Insert(39);
        hashTable.Insert(13);
        hashTable.Insert(16);
        hashTable.Insert(42);
        hashTable.Insert(17);

        hashTable.PrintTable();
        hashTable.HowManyProbes(hashTable);

        Console.ReadLine();
    }
}