using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ClassTypes 
{
    // Backing store for strength
    private int m_strPoints;

    // Strength
    public int strPoints
    {
        get
        {
            return m_strPoints;
        }
        set
        {
            m_strPoints = value;
        }
    }

    // Backing store for constitution
    private int m_conPoints;

    // Constitution
    public int conPoints
    {
        get
        {
            return m_conPoints;
        }
        set
        {
            m_conPoints = value;
        }
    }

    // Backing store for intelligence
    private int m_intPoints;

    // Intelligence
    public int intPoints
    {
        get
        {
            return m_intPoints;
        }
        set
        {
            m_intPoints = value;
        }
    }

    // Backing store for dexterity
    private int m_dexPoints;

    // Dexterity
    public int dexPoints
    {
        get
        {
            return m_dexPoints;
        }
        set
        {
            m_dexPoints = value;
        }
    }

    // Backing store for wisdom
    private int m_wisPoints;

    // Wisdom
    public int wisPoints
    {
        get
        {
            return m_wisPoints;
        }
        set
        {
            m_wisPoints = value;
        }
    }

    // Backing store for charisma
    private int m_chaPoints;

    // Strength
    public int chaPoints
    {
        get
        {
            return m_chaPoints;
        }
        set
        {
            m_chaPoints = value;
        }
    }

    // Backing store for the class name
    private string m_className;

    // Class name
    public string className
    {
        get
        {
            return m_className;
        }
        set
        {
            m_className = value;
        }
    }

    // Instantiate PlayerClass struct with stats
    public ClassTypes(int strPoints, int conPoints, int intPoints, int dexPoints, int wisPoints, int chaPoints, string className)
    {
        m_strPoints = strPoints;
        m_conPoints = conPoints;
        m_intPoints = intPoints;
        m_dexPoints = dexPoints;
        m_wisPoints = wisPoints;
        m_chaPoints = chaPoints;
        m_className = className;
    }

}
