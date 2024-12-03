import mido
import os
import numpy as np

snare_note = 38
kick_note = 35

def extract_note_hit_times(midi_file_path, note):
    hit_times = np.array([])

    # Open the MIDI file
    midi_file = mido.MidiFile(midi_file_path)

    # Define the channel for the drums (usually channel 9)
    drums_channel = 9

    # Get tempo and ticks per beat from the MIDI file
    ticks_per_beat = midi_file.ticks_per_beat
    tempo = 625000  # Default tempo if not found in MIDI file

    # Iterate through each track in the MIDI file
    for track in midi_file.tracks:
        absolute_time = 0
        for msg in track:
            absolute_time += msg.time
            if msg.type == 'set_tempo':
                tempo = msg.tempo


            # Check if the message is a note-on event and on the drums channel
            elif msg.type == 'note_on':
                time_in_ms = mido.tick2second(absolute_time, ticks_per_beat, tempo)
                hit_times = np.append(hit_times, time_in_ms)
    return np.unique(hit_times)

# Example usage
script_dir = os.path.dirname(os.path.abspath(__file__))  # Get directory of the current script
player_attacks_midi_file_path = os.path.join(script_dir, "PlayerAttacks.mid")
enemy_attacks_midi_file_path = os.path.join(script_dir, "EnemyAttacks.mid") 
player_attack_times = extract_note_hit_times(player_attacks_midi_file_path, snare_note)
enemy_attack_times = extract_note_hit_times(enemy_attacks_midi_file_path, kick_note)
output_player_times = file_path = os.path.join(script_dir,"player_attack_times.txt")
output_enemy_times = file_path = os.path.join(script_dir,"enemy_attack_times.txt")

# Write array to text file
np.savetxt(output_player_times, player_attack_times, fmt='%.3f')
np.savetxt(output_enemy_times, enemy_attack_times, fmt='%.3f')