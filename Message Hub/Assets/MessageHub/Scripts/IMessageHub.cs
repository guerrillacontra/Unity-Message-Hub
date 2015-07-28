using System;

namespace IntrovertStudios.Messaging
{
	/// <summary>
	/// Used as a form of communication between entities
	/// so that they can listen, post and react to messages.
	/// 
	/// TKey is the type used for the key for all messages.
	/// 
	/// One of the core aims of the message-hub is to make it type-safe
	/// and predictable (less mistakes!).
	/// </summary>
	public interface IMessageHub<TKey>
	{
		void Connect(TKey id, Action handler);
		void Connect<T>(TKey id, Action<T> handler) where T : class;

		void Disconnect(TKey id, Action handler);
		void Disconnect<T>(TKey id, Action<T> handler) where T : class;

		void DisconnectAll();

		void Post(TKey id);
		void Post<T>(TKey id, T content) where T : class;
	}
}