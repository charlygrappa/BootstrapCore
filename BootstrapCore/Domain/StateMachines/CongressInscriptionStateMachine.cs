using System;
using Stateless;

namespace BootstrapCore.Domain.StateMachines
{
    public class CongressInscriptionStateMachine
    {
        public CongressInscriptionState State { get; set; }
        public StateMachine<CongressInscriptionState, CongressInscriptionTrigger> Machine { get; private set; }
        public StateMachine<CongressInscriptionState, CongressInscriptionTrigger>.TriggerWithParameters<Inscription> AssignTrigger { get; private set; }
        public StateMachine<CongressInscriptionState, CongressInscriptionTrigger>.TriggerWithParameters<Inscription> ApproveTrigger { get; private set; }

        public Inscription Inscription { get; set; }

        public CongressInscriptionStateMachine()
        {
            State = CongressInscriptionState.Created;
       
            Machine = new StateMachine<CongressInscriptionState, CongressInscriptionTrigger>(() => State, s => State = s);
            AssignTrigger = Machine.SetTriggerParameters<Inscription>(CongressInscriptionTrigger.Request);
            ApproveTrigger = Machine.SetTriggerParameters<Inscription>(CongressInscriptionTrigger.Approve);

            Machine.Configure(CongressInscriptionState.Created)
                   .Permit(CongressInscriptionTrigger.Request,CongressInscriptionState.Requested);

            Machine.Configure(CongressInscriptionState.Requested)
                   .SubstateOf(CongressInscriptionState.Created)
                   .OnEntryFrom(AssignTrigger, Inscription => OnRequested(Inscription))
                   .Permit(CongressInscriptionTrigger.Approve, CongressInscriptionState.Approved)
                   .Permit(CongressInscriptionTrigger.Reject, CongressInscriptionState.Rejected);

            Machine.Configure(CongressInscriptionState.Approved)
                   .SubstateOf(CongressInscriptionState.Requested)
                   .OnEntryFrom(ApproveTrigger, Inscription => OnApproved(Inscription))
                   .Permit(CongressInscriptionTrigger.Accounted, CongressInscriptionState.Accounted);
        }

        public void Request()
        {
            Machine.Fire(AssignTrigger,Inscription);
        }

        public void Approve()
        {
            Machine.Fire(ApproveTrigger, Inscription);   
        }

        void OnApproved(Inscription _inscription)
        {
            Console.WriteLine("Approved Inscription for " + _inscription.UserId);
        }

        void OnRequested(Inscription _inscription){
            Console.WriteLine("Requested Inscription from " + _inscription.UserId);
        }


    }
}
