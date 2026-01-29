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
    
    public void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    public void Update()
    {

        if (isAfterBook) {


            if (Input.GetKeyDown(KeyCode.L)) {


                clickedL2++;
            }
        if(clickedL2<2 && touched == true && Input.GetKeyDown(KeyCode.E)) {

                started = true;
                StartDialogue();
                NextSentence();

            }
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

            NextSentence();
        
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

            animator.SetTrigger("EndConversation");
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
    public void OnTriggerExit(Collider other)
    {
        touched = false;
    }

    
}

