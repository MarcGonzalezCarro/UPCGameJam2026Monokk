using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueCaca : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Queue<string> sentences;
    public TMP_Text text;
    public DialogueTriggered Dialogue;
    bool started;
    public bool isAfterBook;
    int clickedL2;
    public Animator animator;
    public GameObject teleport;
    bool ended = false;
    bool hasended = false;
   public GameObject otherDog;
    public GameObject thisDog;
    
    
    public void Start()
    {
        sentences = new Queue<string>();
        if (isAfterBook) {

            teleport.SetActive(false);
        
        
        }
        if (isActiveAndEnabled) {

            otherDog.SetActive(false);
        
        
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (hasended) {

            otherDog.SetActive(true);
            thisDog.SetActive(false);
            return;
        
        }
      
        if (Input.GetKeyDown(KeyCode.L))
        {


            clickedL2++;
        }
        if (isAfterBook) {



           
        if(clickedL2>2 && touched == true && Input.GetKeyDown(KeyCode.E)) {

                started = true;
                StartDialogue();
                NextSentence();

          }
            return;
        }
        if (touched == true && Input.GetKeyDown(KeyCode.E)) {

            started = true;
            StartDialogue();
            NextSentence();
        
        }
        if (started == false)
        {


            return;
        }

        
        if (Input.GetKeyDown(KeyCode.Space)){
            if (!ended) { 
            
            NextSentence();
            return;

            }
            if (ended) {
                NextSentence();

                animator.SetTrigger("EndConversation");
                hasended = true;
                
            }
        }
        
    }

  public  void StartDialogue() {

        animator.SetTrigger("StartConversation");
        sentences.Clear();

        foreach (string sentence in Dialogue.Dialogue.sentences) {

            sentences.Enqueue(sentence);
        
        }
    
    
    }
    public void NextSentence() {

        if (sentences.Count == 0) {

            
            if (isAfterBook) {

                teleport.SetActive(true);
            
            }
            ended = true;
            return;
        
        }
        string sentence = sentences.Dequeue();
        text.text = sentence;
    
    }

    bool touched;
    public void OnTriggerEnter(Collider other)
    {
        touched = true;

    }
    public void OnTriggerStay(Collider other)
    {
        touched = true;

    }
    public void OnTriggerExit(Collider other)
    {
        touched = false;
    }

    
}

