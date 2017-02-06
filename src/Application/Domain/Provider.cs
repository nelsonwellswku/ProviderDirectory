using System;
using Marten.Schema;

namespace Octogami.ProviderDirectory.Application.Domain
{
    public class Provider
    {
        [Identity]
        public Guid ProviderId { get; set; }

        public string NPI { get; set; }

        public EntityType EntityType { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset EnumerationDate { get; set; }

        public DateTimeOffset? DeactivationDate { get; set; }

        public NPIDeactivationReason DeactivationReason { get; set; }

        public DateTimeOffset? ReactivationDate { get; set; }

        public Gender Gender { get; set; }

        public Address MailingAddress { get; set; }

        public Address PracticeAddress { get; set; }
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female,
        Other
    }

    public enum EntityType
    {
        Unknown,
        Individual,
        Organization
    }

    public class Address
    {
        public string LineOne { get; set; }

        public string LineTwo { get; set; }

        public string City { get; set; }

        public State State { get; set; }

        public string Zip { get; set; }
    }

    public class State
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }

    public class NPIDeactivationReason
    {
        public string Code { get; set; }

        public string Text { get; set; }
    }
}