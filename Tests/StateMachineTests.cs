using System;
using BootstrapCore.Domain;
using BootstrapCore.Domain.StateMachines;
using Xunit;

namespace Tests
{
    public class StateMachineTests
    {
        [Fact]
        public void TriggerRequest()
        {
            var i1 = new Inscription() { UserId="Bebis Black" };
            var cism = new CongressInscriptionStateMachine();
            cism.Inscription = i1;

            cism.Request();
            Assert.Equal(cism.State,CongressInscriptionState.Requested);
        }

        [Fact]
        public void CreateMachineInAMiddleState(){
            var i2 = new Inscription() { UserId = "Bebis Il Gatino" };
            var cism = new CongressInscriptionStateMachine();
            cism.Inscription = i2;
            cism.State = CongressInscriptionState.Requested;
            cism.Approve();
            Assert.Equal(cism.State, CongressInscriptionState.Requested);
        }
    }
}
